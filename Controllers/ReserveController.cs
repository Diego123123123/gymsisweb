using GYM.Context;
using GYM.Models;
using GYM.Repositories.Implementations;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace GYM.Controllers
{
    [Produces("application/json")]
    [Route("reserves")]
    public class ReserveController : Controller
    {
        private ReserveRepository reserveRepository;
        private MyAppDbContext myAppDbContext;

        public ReserveController(MyAppDbContext myAppDbContext)
        {
            this.reserveRepository = new ReserveRepository(myAppDbContext);
            this.myAppDbContext = myAppDbContext;
        }

        [HttpGet]
        [EnableCors("MyPolicy")]
        public IActionResult GetAll()
        {
            return Ok(this.reserveRepository.GetAll());
        }

        [HttpGet("{id}", Name = "ReservesByUserId")]
        [EnableCors("MyPolicy")]
        public IActionResult GetByid(int id)
        {
            if (this.reserveRepository.Get(id) == null)
            {
                return BadRequest();
            }
            return Ok(this.reserveRepository.Get(id));
        }

        [HttpGet("users/{id}", Name = "ReservesById")]
        [EnableCors("MyPolicy")]
        public IActionResult GetReservesByUserId(string id)
        {
            return Ok(this.reserveRepository.GetReservasByUserId(id));
        }


        [HttpPost]
        [EnableCors("MyPolicy")]
        public IActionResult Post([FromBody] Reserva reserve)
        {
            if (ModelState.IsValid)
            {
                this.reserveRepository.Add(reserve);
                myAppDbContext.SaveChanges();
                return Ok(new CreatedAtRouteResult("ReservesById", new { id = reserve.ReserveId }, reserve));

            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public IActionResult EditUser([FromBody] Reserva reserve, int id)
        {
            return Ok();
        }

        [HttpDelete("{id}")]
        [EnableCors("MyPolicy")]
        public IActionResult DeleteUser(int id)
        {
            var searchedFunction = this.reserveRepository.Get(id);
            if (searchedFunction == null)
            {
                return BadRequest();
            }
            reserveRepository.Remove(searchedFunction);
            myAppDbContext.SaveChanges();
            return Ok(searchedFunction);
        }
    }
}