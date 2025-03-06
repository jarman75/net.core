using System.Text.Json.Serialization;

namespace LocalChat.Wheather;

public class WeatherResponse
{
    [JsonPropertyName("request")]
    public Request? Request { get; set; }

    [JsonPropertyName("location")]
    public Location? Location { get; set; }

    [JsonPropertyName("current")]
    public Current? Current { get; set; }
}
