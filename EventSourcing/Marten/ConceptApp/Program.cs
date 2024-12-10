using ConceptApp.Services;
using Marten;

const string connectionString = "Host=localhost;Port=5432;Database=events-db;Username=postgres;Password=postgres";
var store = DocumentStore.For(_ =>
{
    _.Connection(connectionString);
    
    // Registrar la proyección para actualizaciones en línea
    //_.Projections.Add<StudentProjection>(ProjectionLifecycle.Inline);
});

var service = new StudentService(store);
var studentId = Guid.Parse("440ac1ea-67bb-419a-81c0-15dbfe54f1f1");
//service.UpdateStudent(studentId, "John Doe").Wait();
var student = service.GetStudent(studentId).Result;
Console.WriteLine(student != null ? $"Student: {student.Name}" : "Student not found");