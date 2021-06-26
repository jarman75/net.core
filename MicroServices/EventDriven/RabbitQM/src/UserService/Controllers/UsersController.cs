using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserService.Data;
using UserService.Entities;
using UserService.Publishers;

namespace UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserServiceContext _context;
        private readonly IntegrationEventSenderService _integrationEventSenderService;

        public UsersController(UserServiceContext context, IntegrationEventSenderService integrationEventSenderService)
        {
            _context = context;
            _integrationEventSenderService = integrationEventSenderService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUser()
        {
            return await _context.User.ToListAsync();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            

            using var transaction = _context.Database.BeginTransaction();
            
            _context.Entry(user).State = EntityState.Modified;            
            await _context.SaveChangesAsync();
            
            var integrationEventData = System.Text.Json.JsonSerializer.Serialize<object>(new { id = user.ID, newname = user.Name, version = user.Version});            

            _context.IntregationEventOutBox.Add(new IntregationEvent { Event = "user.update", Data = integrationEventData });
            
            _context.SaveChanges();
            transaction.Commit();

            _integrationEventSenderService.StartPublishingOutstandingIntegrationEvents();

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            using var transaction = _context.Database.BeginTransaction();

            user.Version = 1;
            _context.User.Add(user);
            await _context.SaveChangesAsync();

            var integrationEventData = System.Text.Json.JsonSerializer.Serialize<object>(new { id = user.ID, name = user.Name, version = user.Version});            

            _context.IntregationEventOutBox.Add(new IntregationEvent { Event = "user.add", Data = integrationEventData });

            _context.SaveChanges();
            transaction.Commit();

            _integrationEventSenderService.StartPublishingOutstandingIntegrationEvents();

            return CreatedAtAction("GetUser", new { id = user.ID }, user);
        }
    }
}