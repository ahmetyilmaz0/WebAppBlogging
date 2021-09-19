using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppBlogging.Models;

namespace WebAppBlogging.Service
{
    public class EntityService : IEntityService
    {
        private readonly BloggingDbContext _context;
        public EntityService(BloggingDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Entities>> GetEntities()
        {
            return await _context.Entities.Include(x => x.User).ToListAsync();
        }

        public async Task<Entities> AddEntity(Entities entity)
        {
            _context.Entities.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteEntity(Entities entity)
        {
            _context.Entities.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<Entities> GetEntityById(int id)
        {
            return await _context.Entities.Include(x => x.User).FirstOrDefaultAsync(x => x.EntityID == id);
        }
        public async Task<IEnumerable<Entities>> GetEntityByUserName(string username)
        {
            return await _context.Entities.Include(x => x.User).Where(x => x.User.UserName == username).ToListAsync();
        }
    }
}
