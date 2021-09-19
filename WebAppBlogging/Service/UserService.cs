using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppBlogging.Models;

namespace WebAppBlogging.Service
{
    public class UserService : IUserService
    {
        private readonly BloggingDbContext _context;
        public UserService(BloggingDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Users>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<Users> AddUser(Users user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task DeleteUser(Users user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<Users> GetUserById(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.UserID == id);
        }    

        public async Task<Users> GetUserByName(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.UserName == username);
        }

        public Task<bool> UserLogin(LoginModel model)
        {
            try
            {
                var user = _context.Users.FirstOrDefaultAsync(x => x.UserName == model.UserName).Result;
                if (user != null)
                    if (user.Password == model.Password)
                        return Task.FromResult(true);
                return Task.FromResult(false);
            }
            catch (Exception)
            {
                return Task.FromResult(false);
            }
            
        }
    }
}
