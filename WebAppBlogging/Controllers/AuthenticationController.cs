using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebAppBlogging.Models;
using WebAppBlogging.Service;

namespace WebAppBlogging.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ApplicationSettings _appSettings;
        public AuthenticationController(IUserService userService, IOptions<ApplicationSettings> appSettings)
        {
            _userService = userService;
            _appSettings = appSettings.Value;
        }

        [HttpPost]
        [Route("Register")]
        //POST : /api/Authentication/Register
        public async Task<Object> Register(Users model)
        {
            try
            {
                model.UserRole = "user";
                var result = await _userService.AddUser(model);
                return Ok(result);
            }
            catch
            {
                return BadRequest(new { message = "Username or password is incorrect." });
            }
        }

        [HttpPost("Login")]
        //POST : /api/Authentication/Login
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (await _userService.UserLogin(model))
            {
                var user = _userService.GetUserByName(model.UserName).Result;
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("UserID",user.UserID.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.JWT_Secret)), SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);
                return Ok(new { token });
            }
            else
                return BadRequest(new { message = "Username or password is incorrect." });
        }
    }
}
