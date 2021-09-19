using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppBlogging.Models;
using WebAppBlogging.Service;

namespace WebAppBlogging.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
       
        // GET: api/Users
        [HttpGet]
        public async Task<IEnumerable<Users>> GetUsers()
        {
            return await _userService.GetUsers();
        }

        // GET: api/Users/username
        [HttpGet("{username}")]
        public async Task<ActionResult<Users>> GetUsers(string username)
        {
            var users = await _userService.GetUserByName(username);

            if (users == null)
            {
                return NotFound();
            }

            return users;
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Users>> PostUsers(Users users)
        {
            if (!UsersExists(users.UserName))
            {
                await _userService.AddUser(users);
                return CreatedAtAction("GetUsers", new { id = users.UserID }, users);
            }
            else return NotFound();
        }

        private bool UsersExists(string username)
        {
            return _userService.GetUsers().Result.Any(e => e.UserName == username);
        }
    }
}
