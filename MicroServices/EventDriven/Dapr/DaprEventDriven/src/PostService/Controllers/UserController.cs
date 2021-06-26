using Microsoft.AspNetCore.Mvc;
using PostService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PostService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetPost()
        {
            return NoContent();
        }
    }
}