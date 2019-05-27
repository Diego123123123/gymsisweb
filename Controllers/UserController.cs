using GYM.Context;
using GYM.Models;
using GYM.Repositories.Implementations;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GYM.Controllers
{
    [Produces("application/json")]
    [Route("users")]
    public class UserController : Controller
    {

        private UserRepositoryImpl userRepository;
        private MyAppDbContext myAppDbContext;
        private readonly UserManager<IdentityClient> _userManager;
        private readonly SignInManager<IdentityClient> _signInManager;

        public UserController(MyAppDbContext myAppDbContext, UserManager<IdentityClient> userManager
        ,SignInManager<IdentityClient> signinmanager)
        {
            this.userRepository = new UserRepositoryImpl(myAppDbContext);
            this.myAppDbContext = myAppDbContext;
            this._signInManager = signinmanager;
            this._userManager = userManager;
        }

        [HttpGet]
        [EnableCors("MyPolicy")]
        public IActionResult GetAll()
        {
            var users = this.userRepository.GetAll();
            return Ok(users);
        }

        [HttpGet("{id}", Name = "UserByID")]
        public IActionResult GetByid(int id)
        {
            if (this.userRepository.Get(id) == null)
            {
                return BadRequest();
            }
            return Ok(this.userRepository.Get(id));
        }


        [HttpPost]
        public IActionResult Post([FromBody] User user)
        {
            if (ModelState.IsValid)
            {
                userRepository.Add(user);
                myAppDbContext.SaveChanges();
                return Ok(new CreatedAtRouteResult("UserByID", new { id = user.UserId }, user));
             
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public IActionResult EditUser([FromBody] User user, int id)
        {
            if (ModelState.IsValid)
            {
                userRepository.Update(id, user);
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var searchedUser = userRepository.Get(id);
            if (searchedUser == null)
            {
                return BadRequest();
            }
            userRepository.Remove(searchedUser);
            myAppDbContext.SaveChanges();
            return Ok(searchedUser);
        }


    }
}