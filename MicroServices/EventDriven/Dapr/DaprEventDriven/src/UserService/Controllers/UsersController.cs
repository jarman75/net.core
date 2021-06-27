using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using UserService.Models;

namespace UserService.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserRepository _repository;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IUserRepository repository, ILogger<UsersController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(User), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<User>> GetUserByIdAsync(string id)
        {
            var user = await _repository.GetUserAsync(id);

            return Ok(user ?? new User(id));
        }

        [HttpPost]
        [ProducesResponseType(typeof(User), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<User>> PostUserAsync(User value)
        {   
            return Ok(await _repository.UpdateUserAsync(value));
        }

        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult> DeleteUser(string id)
        {
            await _repository.DeleteUserAsync(id);
            return Ok();
        }
    }
}