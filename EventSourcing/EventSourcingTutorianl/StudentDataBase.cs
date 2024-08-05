using EventSourcingTutorianl.Events;

namespace EventSourcingTutorianl;

public class StudentDataBase
{
    readonly Dictionary<Guid, SortedList<DateTime, Event>> _studentsEvents = [];
    
    public void Append(Event @event)
    {
        //find student event 
        var stream = _studentsEvents!.GetValueOrDefault(@event.StreamId, null);

        if (stream is null)
        {
            _studentsEvents[@event.StreamId] = new SortedList<DateTime, Event>();
        }
        @event.CreatedAtUTC = DateTime.UtcNow;
        _studentsEvents[@event.StreamId].Add(@event.CreatedAtUTC, @event);        
    }
    public Student? GetStudent(Guid studentId)
    {
        if (!_studentsEvents.ContainsKey(studentId))
        {
            return null;
        }
        var student = new Student();
        var studentEvents = _studentsEvents[studentId];
        foreach (var eventItem in studentEvents)
        {
            student.Apply(eventItem.Value);
        }
        return student;
    }
}