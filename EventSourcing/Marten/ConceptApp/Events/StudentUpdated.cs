namespace ConceptApp.Events;

public sealed record StudentUpdated(Guid Id, string Name, DateTime UpdatedAt);