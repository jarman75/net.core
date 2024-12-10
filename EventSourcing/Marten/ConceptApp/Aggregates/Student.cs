using ConceptApp.Events;
namespace ConceptApp.Aggregates;

public class Student(StudentCreated @event)
{
    public Guid Id { get; private set; } = @event.Id;
    public string Name { get; private set; } = @event.Name;
    public DateTime CreatedAt { get; private set; } = @event.CreatedAt;
    public DateTime? UpdatedAt { get; private set; }

    // Método Apply para el evento StudentUpdated
    public void Apply(StudentUpdated @event)
    {
        Name = @event.Name;
        UpdatedAt = @event.UpdatedAt;
    }
}