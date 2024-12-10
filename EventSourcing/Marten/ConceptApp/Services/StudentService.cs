using ConceptApp.Aggregates;
using ConceptApp.Events;
using Marten;

namespace ConceptApp.Services;

public class StudentService(IDocumentStore store)
{
    //crear un nuevo estudiante
    public async Task CreateStudent(Guid studentId, string name)
    {
        await using var session = store.LightweightSession();
        var studentCreated = new StudentCreated(studentId, name, DateTime.UtcNow);
        session.Events.StartStream(studentId, studentCreated);
        await session.SaveChangesAsync();
    }
    //actualizar estudiante
    public async Task UpdateStudent(Guid studentId, string name)
    {
        await using var session = store.LightweightSession();
        var studentUpdated = new StudentUpdated(studentId, name, DateTime.UtcNow);
        session.Events.Append(studentId, studentUpdated);
        await session.SaveChangesAsync();
    }
    public async Task<Student?> GetStudent(Guid studentId)
    {
        await using var session = store.QuerySession();
        var student = await session.Events.AggregateStreamAsync<Student>(studentId);
        return student;
    }
}