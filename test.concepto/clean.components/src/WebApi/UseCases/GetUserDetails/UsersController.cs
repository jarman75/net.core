using Application.Boundaries.GetUserDetails;
using FluentMediator;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace WebApi.UseCases.GetUserDetails
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly GetUserDetailsPresenter _presenter;

        public UsersController(IMediator mediator, GetUserDetailsPresenter presenter)
        {
            _mediator = mediator;
            _presenter = presenter;
        }

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpGet("{UserId}", Name = "GetUser")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetUserDetailsResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUser([FromRoute][Required] GetUserDetailsRequest request)
        {
            var input = new GetUserDetailsInput(request.UserId);
            await _mediator.PublishAsync(input);
            return _presenter.ViewModel;
        }

    }
}