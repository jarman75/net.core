using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Application.Boundaries.CreateUser;
using Domain.ValueObjects;
using FluentMediator;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.UseCases.CreateUser
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly CreateUserPresenter _presenter;

        public UsersController(IMediator mediator, CreateUserPresenter presenter)
        {
            _mediator = mediator;
            _presenter = presenter;
        }

        /// <summary>
        /// Creates the user.
        /// </summary>
        /// <response code="200">The registered user was create successfully.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="500">Error.</response>
        /// <param name="request">The request to register a user</param>
        /// <returns>The newly registered user</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CreateUserResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody][Required] CreateUserRequest request)
        {                                                
            var input = new CreateUserInput(new ShortName(request.Name), new Email(request.Email), new Password(request.Password));
            await _mediator.PublishAsync(input);
            return _presenter.ViewModel;
        }
    }
}