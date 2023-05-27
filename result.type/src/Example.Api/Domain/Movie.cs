namespace Example.Api.Domain;

public class Movie
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public required string Director { get; set; }
    public int? ReleaseYear { get; set; }   
}