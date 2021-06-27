using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PostService.Models;
using System.Net;
using System.Threading.Tasks;

namespace PostService.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {

        private IPostRepository _repository;
        private readonly ILogger<PostsController> _logger;

        public PostsController(IPostRepository repository, ILogger<PostsController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Post), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Post>> GetPostAsync(string id)
        {
            var post = await _repository.GetPostAsync(id);

            return Ok(post ?? new Post(id));
        }
        
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<Post>> PostPostAsync(Post post)
        {
            return Ok(await _repository.UpdatePostAsync(post));
        }
    }
}