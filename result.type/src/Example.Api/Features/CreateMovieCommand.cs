using Example.Api.Domain;
using MediatR;
using FluentValidation;

namespace Example.Api.Features;

public class CreateMovieCommand : IRequest<Result<Movie, ErrorResult>>
{
    public required string Title { get; set; }   
    public required string Director { get; set; }
    public int? ReleaseYear { get; set; }   
}
public class CreateMovieValidator : AbstractValidator<CreateMovieCommand>
{
    public CreateMovieValidator()
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

public class CreateMovieCommandHandler : IRequestHandler<CreateMovieCommand, Result<Movie, ErrorResult>>
{
    

    public async Task<Result<Movie, ErrorResult>> Handle(CreateMovieCommand request, CancellationToken cancellationToken)
    {
        
        if (request.Title == "x")
        {
            return new ErrorResult(ErrorCode.Unauthorized, "User not authorized");
        }

        var validator = new CreateMovieValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return new ErrorResult(ErrorCode.ValidationError, validationResult?.Errors?.FirstOrDefault()?.ErrorMessage ?? "Validation error");
        }

        if(request.Director == "the invisible") {
            return new ErrorResult(ErrorCode.NotFound, "Director");
        }



        var movie = new Movie
        {
            Id = Guid.NewGuid(),
            Director = request.Director,
            Title = request.Title,
            ReleaseYear = request.ReleaseYear
        };

        return movie;

    }

    
}
