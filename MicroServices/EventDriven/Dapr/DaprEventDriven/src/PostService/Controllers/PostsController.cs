using Microsoft.AspNetCore.Mvc;
using PostService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PostService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {        

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Post>>> GetPost()
        {
            return NoContent();
        }
        
        [HttpPost]
        public async Task<ActionResult<Post>> PostPost(Post post)
        {
            return NoContent();
        }
    }
}