using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PostService.Data;
using PostService.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PostService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly PostServiceContext _context;

        public UsersController(PostServiceContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetPost()
        {
            return await _context.User.ToListAsync();
        }
    }
}