using Example.Api.Domain;
using FluentValidation;
using MediatR;

namespace Example.Api.Features;

public class UpdateMovieCommand : IRequest<Result<Unit,ValidationFailed>>
{
    
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public required string Director { get; set;}
    public int? ReleaseYear { get; }    
}

public class UpdateMovieCommandValidator : AbstractValidator<UpdateMovieCommand>
{
    public UpdateMovieCommandValidator()
    {
        //title must not be empty and minimum length is 2
        //directo must not be empty and minimum two words
        //release year when not is null must be greater than 1890 (first movie ever made) and less than actual year (no future movies)
        
        RuleFor(x => x.Title)
            .NotEmpty()
            .MinimumLength(2);
        
        RuleFor(x => x.Director)
            .NotEmpty()
            .MinimumLength(3)
            .Must(x => x.Split(" ").Length >= 2);
        
        RuleFor(x => x.ReleaseYear)
            .GreaterThan(1890)
            .LessThanOrEqualTo(DateTime.Now.Year)
            .When(x => x.ReleaseYear.HasValue);
    }
}

public class UpdateMovieCommandHandler : IRequestHandler<UpdateMovieCommand, Result<Unit,ValidationFailed>>
{
    public async Task<Result<Unit,ValidationFailed>> Handle(UpdateMovieCommand request, CancellationToken cancellationToken)
    {
        if (request.Id == Guid.Empty)
        {
            throw new NotFoundException("Movie", request.Id.ToString());
        }
        
        if (request.Director == "the invisible")
        {
            return new ValidationFailed("Director", "The director invisible is not allowed");
        }
        var validator = new UpdateMovieCommandValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return new ValidationFailed(validationResult);
        }

        return Unit.Value;
    }
}
