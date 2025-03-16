using System.Text.Json;

namespace LocalChat.Wheather;

public class WeatherClient
{
    private readonly HttpClient _httpClient;

    public WeatherClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> GetCurrentWeatherAsync(string location, string unit)
    {
        // Replace with the actual API endpoint and parameters    
        var response = await _httpClient.GetAsync($"http://api.weatherstack.com/current?access_key=ed200b16db5120e80a19ca5200056230&query={location}&units=m");

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var weatherData = JsonSerializer.Deserialize<WeatherResponse>(content);
            if (weatherData is null)
            {
                return "Unable to fetch weather data";
            }
            return $"{weatherData.Location?.Name}: {weatherData.Current?.WeatherDescriptions?[0]}, {weatherData.Current?.Temperature} {unit}";
        }
        else
        {
            return "Unable to fetch weather data";
        }
    }
}
