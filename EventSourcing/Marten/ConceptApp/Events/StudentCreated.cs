using System.Runtime.InteropServices.JavaScript;

namespace ConceptApp.Events;

public sealed record StudentCreated(Guid Id, string Name, DateTime CreatedAt);