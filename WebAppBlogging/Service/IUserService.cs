using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppBlogging.Models;

namespace WebAppBlogging.Service
{
    public interface IUserService
    {
        Task<IEnumerable<Users>> GetUsers();
        Task<Users> GetUserById(int id);
        Task<Users> AddUser(Users user);
        Task DeleteUser(Users user);
        Task<Users> GetUserByName(string username);
        Task<bool> UserLogin(LoginModel model);
    }
}
