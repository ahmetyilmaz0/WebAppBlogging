using System;
using System.Collections.Generic;
using System.Linq;
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
    public class EntitiesController : ControllerBase
    {
        private readonly IEntityService _entityService;
        private readonly IUserService _userService;

        public EntitiesController(IEntityService entityService,IUserService userService)
        {
            _entityService = entityService;
            _userService = userService;
        }

        // GET: api/Entities
        [HttpGet]
        public async Task<IEnumerable<Entities>> GetEntities()
        {
            return await _entityService.GetEntities();
        }

        // GET: api/Entities/username
        [HttpGet("{username}")]
        public async Task<IEnumerable<Entities>> GetUserEntities(string username)
        {
            return await _entityService.GetEntityByUserName(username);
        }

        // POST: api/Entities
        [HttpPost]
        public async Task<ActionResult<Entities>> PostEntities(EntityModel entity)
        {
            Entities entities = new Entities()
            {
                Entity = entity.Entity,
                User = _userService.GetUserByName(entity.UserName).Result
            };
            await _entityService.AddEntity(entities);
            return CreatedAtAction("GetEntities", new { id = entities.EntityID }, entities);
        }

        // DELETE: api/Entities/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEntities(int id)
        {
            var entity = await _entityService.GetEntityById(id);
            if (entity == null)
            {
                return NotFound();
            }
            await _entityService.DeleteEntity(entity);
            return NoContent();
        }
    }
}
