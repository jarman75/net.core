using Example.Api.Features;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Example.Api.Controllers;

public class BaseApiController : ControllerBase
{
    protected IActionResult GetErrorActionResult(ErrorResult failure)
    {
        return failure.ErrorCode switch
        {
            ErrorCode.Unauthorized => Unauthorized(failure),
            ErrorCode.Forbidden => Forbid(),
            ErrorCode.ValidationError => BadRequest(failure),
            ErrorCode.NotFound => NotFound(failure),                       
            _ => StatusCode(500, failure)
        };
    }
}

[ApiController]
[Route("api/[controller]")]
public class MoviesController : BaseApiController
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
            success => Ok(success),
            failure => GetErrorActionResult(failure)            
        );
    }

    
}