using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppBlogging.Models;

namespace WebAppBlogging.Service
{
    public interface IEntityService
    {
        Task<IEnumerable<Entities>> GetEntities();
        Task<Entities> GetEntityById(int id);
        Task<Entities> AddEntity(Entities entity);
        Task DeleteEntity(Entities entity);
        Task<IEnumerable<Entities>> GetEntityByUserName(string username);
    }
}
