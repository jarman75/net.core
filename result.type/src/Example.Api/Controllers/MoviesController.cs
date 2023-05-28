using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Example.Api.Features;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Example.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MoviesController : ControllerBase
{
    readonly IMediator _mediator;

    public MoviesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    //post for create movie usin create movie command
    [HttpPost]
    public async Task<IActionResult> CreateMovie(CreateMovieCommand request)
    {
        var result = await _mediator.Send(request);
        return result.Match<IActionResult>(
            success => CreatedAtAction(nameof(GetMovie), new {id = success.Id}, success),
            failure => BadRequest(failure.ValidationResult.Errors.Select(x => x.ErrorMessage)));
    }

    //get for get movie by id
    [HttpGet("{id}")]
    public async Task<IActionResult> GetMovie(Guid id)
    {
        var result = await _mediator.Send(new GetMovieByIdQuery(id));
        return result.Match<IActionResult>(
            success => Ok(success),
            failure => NotFound());
    }
    //update movie using update movie command
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMovie(Guid id, UpdateMovieCommand request)
    {
        request.Id = id;
        var result = await _mediator.Send(request);
        return result.Match<IActionResult>(
            success => NoContent(),
            failure => BadRequest(failure.ValidationResult.Errors.Select(x => x.ErrorMessage)));
    }
   //get a empty guid
   // var guid= "00000000-0000-0000-0000-000000000000"; 
   
}