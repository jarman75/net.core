using Example.Api.Domain;
using MediatR;

namespace Example.Api.Features;

public class GetMovieByIdQuery : IRequest<Result<Movie?,NotFoundFailed>>
{
    public GetMovieByIdQuery(Guid id) => Id = id;
    public Guid Id { get; }
}

public class GetMovieByIdQueryHandler : IRequestHandler<GetMovieByIdQuery, Result<Movie?,NotFoundFailed>>
{
    public async Task<Result<Movie?,NotFoundFailed>> Handle(GetMovieByIdQuery request, CancellationToken cancellationToken)
    {
        if (request.Id == Guid.Empty)
        {
            return new NotFoundFailed(request.Id);
        }
        
        var movie = new Movie
        {
            Id = request.Id,
            Director = "The Wachowskis",
            Title = "The Matrix",
            ReleaseYear = 1999
        };
        return await  Task.FromResult(movie);
    }
}
