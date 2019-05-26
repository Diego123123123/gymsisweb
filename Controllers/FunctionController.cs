using GYM.Context;
using GYM.Models;
using GYM.Repositories.Implementations;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace GYM.Controllers
{
    [Produces("application/json")]
    [Route("functions")]
    public class FunctionController : Controller
    {
        private FunctionRepository functionRepository;
        private MyAppDbContext myAppDbContext;

        public FunctionController(MyAppDbContext myAppDbContext)
        {
            this.functionRepository = new FunctionRepository(myAppDbContext);
            this.myAppDbContext = myAppDbContext;
        }

        [HttpGet]
        [EnableCors("MyPolicy")]
        public IActionResult GetAll()
        {
            return Ok(this.functionRepository.GetAll());
        }

        [HttpGet("{id}", Name = "FunctionsById")]
        [EnableCors("MyPolicy")]
        public IActionResult GetByid(int id)
        {
            if (this.functionRepository.Get(id) == null)
            {
                return BadRequest();
            }
            return Ok(this.functionRepository.Get(id));
        }


        [HttpPost]
        [EnableCors("MyPolicy")]
        public IActionResult Post([FromBody] Function function)
        {
            if (ModelState.IsValid)
            {
                this.functionRepository.Add(function);
                myAppDbContext.SaveChanges();
                return Ok(new CreatedAtRouteResult("FunctionsById", new { id = function.FunctionId }, function));

            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        [EnableCors("MyPolicy")]
        public IActionResult EditFunction([FromBody] Function function, int id)
        {
            if (ModelState.IsValid)
            {
                functionRepository.Update(id, function);
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        [EnableCors("MyPolicy")]
        public IActionResult DeleteUser(int id)
        {
            var searchedFunction = this.functionRepository.Get(id);
            if (searchedFunction == null)
            {
                return BadRequest();
            }
            functionRepository.Remove(searchedFunction);
            myAppDbContext.SaveChanges();
            return Ok(searchedFunction);
        }
    }
}