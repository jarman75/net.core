using System.Text.Json.Serialization;

namespace LocalChat.Wheather;

public class Request
{
    [JsonPropertyName("type")]
    public string? Type { get; set; }

    [JsonPropertyName("query")]
    public string? Query { get; set; }

    [JsonPropertyName("language")]
    public string? Language { get; set; }

    [JsonPropertyName("unit")]
    public string? Unit { get; set; }
}
