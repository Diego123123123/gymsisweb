using GYM.Context;
using GYM.Models;
using GYM.Repositories.Implementations;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GYM.Controllers
{
    [Produces("application/json")]
    [Route("reserves")]
    public class ReserveController : Controller
    {
        private ReserveRepository reserveRepository;
        private MyAppDbContext myAppDbContext;
        private FunctionRepository functionRepository;
        private readonly UserManager<IdentityClient> _userManager;
        public ReserveController(MyAppDbContext myAppDbContext, UserManager<IdentityClient> userManager)
        {
            this.functionRepository = new FunctionRepository(myAppDbContext);
            this.reserveRepository = new ReserveRepository(myAppDbContext);
            this.myAppDbContext = myAppDbContext;
            this._userManager = userManager;
        }

        [HttpGet]
        [EnableCors("MyPolicy")]
        public IActionResult GetAll()
        {
            return Ok(this.reserveRepository.GetAll());
        }

        [HttpGet]
        [EnableCors("MyPolicy")]
        [Route("userreserves")]
        public async Task<IActionResult> GetUserReserves()
        {
            var reserves = this.reserveRepository.GetAll().ToList();
            var resp = new List<UserReserve>();
            for (int i = 0; i < reserves.Count; i++)
            {
                var us = new UserReserve();
                us.ReserveId = reserves[i].ReserveId;
                us.function = this.functionRepository.GetAll().Where(f => f.FunctionId == reserves[i].FunctionId).FirstOrDefault().MovieName;
                us.amount = reserves[i].AmountOfPeople;
                us.email = this._userManager.Users.Where(u => u.Id == reserves[i].User).FirstOrDefault().Email;
                resp.Add(us);
            }
            return Ok(resp);
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
        [EnableCors("MyPolicy")]
        public IActionResult EditUser([FromBody] Reserva reserve, int id)
        {
            if (ModelState.IsValid)
            {
                reserveRepository.Update(id, reserve);
                return Ok();
            }
            return BadRequest();
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