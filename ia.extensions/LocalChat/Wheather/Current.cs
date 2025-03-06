using System.Text.Json.Serialization;

namespace LocalChat.Wheather;

public class Current
{
    [JsonPropertyName("observation_time")]
    public string? ObservationTime { get; set; }

    [JsonPropertyName("temperature")]
    public int Temperature { get; set; }

    [JsonPropertyName("weather_code")]
    public int WeatherCode { get; set; }

    [JsonPropertyName("weather_icons")]
    public string[]? WeatherIcons { get; set; }

    [JsonPropertyName("weather_descriptions")]
    public string[]? WeatherDescriptions { get; set; }

    [JsonPropertyName("wind_speed")]
    public int WindSpeed { get; set; }

    [JsonPropertyName("wind_degree")]
    public int WindDegree { get; set; }

    [JsonPropertyName("wind_dir")]
    public string? WindDir { get; set; }

    [JsonPropertyName("pressure")]
    public int Pressure { get; set; }

    [JsonPropertyName("precip")]
    public double Precip { get; set; }

    [JsonPropertyName("humidity")]
    public int Humidity { get; set; }

    [JsonPropertyName("cloudcover")]
    public int Cloudcover { get; set; }

    [JsonPropertyName("feelslike")]
    public int Feelslike { get; set; }

    [JsonPropertyName("uv_index")]
    public int UvIndex { get; set; }

    [JsonPropertyName("visibility")]
    public int Visibility { get; set; }
}
