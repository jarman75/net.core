namespace EventSourcingTutorianl.Events;

public abstract class Event
{
    
    public abstract Guid StreamId { get; }
    public DateTime CreatedAtUTC { get; set; }
}