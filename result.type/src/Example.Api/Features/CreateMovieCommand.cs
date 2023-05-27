using Example.Api.Domain;
using MediatR;
using FluentValidation;

namespace Example.Api.Features;

public class CreateMovieCommand : IRequest<Result<Movie, ValidationFailed>>
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

public class CreateMovieCommandHandler : IRequestHandler<CreateMovieCommand, Result<Movie, ValidationFailed>>
{
    private readonly IMediator _mediator;

    public CreateMovieCommandHandler(IMediator mediator) => _mediator = mediator;

    public async Task<Result<Movie, ValidationFailed>> Handle(CreateMovieCommand request, CancellationToken cancellationToken)
    {
        

        var validator = new CreateMovieValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return new ValidationFailed(validationResult);
        }

        if(request.Director == "the invisible") {
            return new ValidationFailed("Director", "The director invisible is not allowed");
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
