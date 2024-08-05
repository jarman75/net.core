using EventSourcingTutorianl;
using EventSourcingTutorianl.Events;

var studentDatabase = new StudentDataBase();

var studentId = Guid.Parse("b4e0a6b7-6b7d-4b6b-8b6b-6b6b6b6b6b6b");

//student created event
var studentCreated = new StudentCreated
{
    StudentId = studentId,
    Email = "jhon.doe@example.com", 
    FullName = "John Doe",
    DateOfBirth = new DateTime(1993, 1, 1)    
};   
studentDatabase.Append(studentCreated);

//student enrolled event
var studentEnrolled = new StudentEnrolled
{
    StudentId = studentId,  
    CourseName = "C# Fundamentals"
};
studentDatabase.Append(studentEnrolled);

//student enrolled event
studentEnrolled = new StudentEnrolled
{
    StudentId = studentId,  
    CourseName = "Eventsourcing Tutorial"
};
studentDatabase.Append(studentEnrolled);

//student enrolled event
studentEnrolled = new StudentEnrolled
{
    StudentId = studentId,  
    CourseName = "Java Fundamentals"
};
studentDatabase.Append(studentEnrolled);

//student updated event
var studentUpdated = new StudentUpdated
{
    StudentId = studentId,
    Email = "jhon.doe@gmail.com",
    FullName = "John Doe",
    
};
studentDatabase.Append(studentUpdated);

//student unenrolled event
var studentUnEnrolled = new StudentUnEnrolled
{
    StudentId = studentId,  
    CourseName = "Java Fundamentals"
};
studentDatabase.Append(studentUnEnrolled);

var student = studentDatabase.GetStudent(studentId);
if (student is null) {
    Console.WriteLine("Student not found");
} 
else 
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


