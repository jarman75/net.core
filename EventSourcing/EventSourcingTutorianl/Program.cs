using EventSourcingTutorianl;
using EventSourcingTutorianl.Events;
using Microsoft.Extensions.Configuration;

IConfiguration configuration = new ConfigurationBuilder()
    .AddUserSecrets<Program>()
    .Build();

var studentDatabase = new StudentDataBase(configuration);

var studentId = Guid.Parse("b4e0a6b7-6b7d-4b6b-8b6b-6b6b6b6b6b6b");

var studentCreated = new StudentCreated
{
    StudentId = studentId,
    Email = "jhon.doe@example.com", 
    FullName = "John Doe",
    DateOfBirth = new DateTime(1990, 1, 1)    
};   
await studentDatabase.AppendAsync(studentCreated);


var studentEnrolled = new StudentEnrolled
{
    StudentId = studentId,  
    CourseName = "C# Fundamentals"
};
await studentDatabase.AppendAsync(studentEnrolled);



var studentUpdated = new StudentUpdated
{
    StudentId = studentId,
    Email = "jhon.doe@gmail.com",
    FullName = "John Doe",
  
};
await studentDatabase.AppendAsync(studentUpdated);



var student = await studentDatabase.GetStudentAsync(studentId);
if (student is null) 
{
    Console.WriteLine("Student not found");
} 
else 
{
    WriteStudent(student);
}




static void WriteStudent(Student student)
{
    Console.WriteLine("Student: {0}", student.Id);
    Console.WriteLine("Email: {0}", student.Email);
    Console.WriteLine("Name: {0}", student.FullName);
    Console.WriteLine("Date of birth: {0}", student.DateOfBirth);
    Console.WriteLine("Enrolled courses:");
    foreach (var course in student.EnrolledCourses)
    {
        Console.WriteLine("- {0}", course);
    }    
}