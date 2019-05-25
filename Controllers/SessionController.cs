using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using GYM.Context;
using GYM.Models;
using GYM.Repositories.Implementations;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace GYM.Controllers
{
    public class SessionController : Controller
    {
        private readonly UserManager<IdentityClient> _userManager;
        private readonly SignInManager<IdentityClient> _signInManager;
        private readonly IConfiguration _configuration;
        private UserRepositoryImpl userRepository;
        private MyAppDbContext myAppDbContext;

        public SessionController(
            UserManager<IdentityClient> userManager,
            SignInManager<IdentityClient> signInManager,
            IConfiguration configuration, MyAppDbContext myAppDbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            this._configuration = configuration;
            this.userRepository = new UserRepositoryImpl(myAppDbContext);
            this.myAppDbContext = myAppDbContext;
        }

        [Route("users/create")]
        [EnableCors("MyPolicy")]
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserInfoForSignUp model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityClient { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var uid = "";
                    myAppDbContext.Add(new User { Email = model.Email, LastName = model.LastName, Name = model.Name, Password = model.Password });
                    await myAppDbContext.SaveChangesAsync();
                    var users = await this._userManager.Users.ToListAsync();
                    for (int i = 0; i < users.Count; i++)
                    {
                        if (users[i].Email == model.Email)
                        {
                            uid = users[i].Id;
                        }
                    }

                    return BuildToken(new UserInfo { Email=model.Email, Password=model.Password}, uid);
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest(ModelState);
            }

        }

        [HttpPost]
        [EnableCors("MyPolicy")]
        [Route("users/login")]
        public async Task<IActionResult> Login([FromBody] UserInfo userInfo)
        {
            if (ModelState.IsValid)
            {
                var uid = "";
                var result = await _signInManager.PasswordSignInAsync(userInfo.Email, userInfo.Password, isPersistent: false, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    var users = await this._userManager.Users.ToListAsync();
                    for (int i = 0; i < users.Count; i++)
                    {
                        if (users[i].Email == userInfo.Email)
                        {
                            uid = users[i].Id;
                        }
                    }
                    return BuildToken(userInfo, uid);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return BadRequest(ModelState);
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        private IActionResult BuildToken(UserInfo userInfo, string userid)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, userInfo.Email),
                new Claim("userid", userid),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["sisweb_key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddHours(1);

            JwtSecurityToken token = new JwtSecurityToken(
               issuer: "yourdomain.com",
               audience: "yourdomain.com",
               claims: claims,
               expires: expiration,
               signingCredentials: creds);

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = expiration
            });

        }
    }
}