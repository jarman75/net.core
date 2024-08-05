using System.Text.Json.Serialization;

namespace EventSourcingTutorianl.Events;

[JsonPolymorphic]
[JsonDerivedType(typeof(StudentCreated), nameof(StudentCreated))]
[JsonDerivedType(typeof(StudentUpdated), nameof(StudentUpdated))]
[JsonDerivedType(typeof(StudentEnrolled), nameof(StudentEnrolled))]
[JsonDerivedType(typeof(StudentUnEnrolled), nameof(StudentUnEnrolled))]
public abstract class Event
{
        
    public abstract Guid StreamId { get; }
    public DateTime CreatedAtUTC { get; set; }

    [JsonPropertyName("pk")] public string Pk => StreamId.ToString();
    [JsonPropertyName("sk")] public string Sk => CreatedAtUTC.ToString("O");
}