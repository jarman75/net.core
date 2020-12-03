using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Application.UseCases.UpdateUser;
using Domain.ValueObjects;
using FluentMediator;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.UseCases.UpdateUser
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly UpdateUserPresenter _presenter;

        public UsersController(IMediator mediator, UpdateUserPresenter presenter)
        {
            _mediator = mediator;
            _presenter = presenter;
        }

        /// <summary>
        /// Updates the user.
        /// </summary>
        /// <response code="200">The registered user was update successfully.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="404">The user not exist.</response>
        /// <response code="500">Error.</response>
        /// <param name="request">The request to update a user</param>
        /// <returns>The update registered user</returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UpdateUserResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put([FromBody][Required] UpdateUserRequest request)
        {
            var input = new UpdateUserInput(request.UserId, new ShortName(request.Name), new Email(request.Email));
            await _mediator.PublishAsync(input);
            return _presenter.ViewModel;
        }
    }
}