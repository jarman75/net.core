namespace Integration.Test;
public class IntegrationTest : IClassFixture<CustomFactory>
{
    private readonly CustomFactory _factory;

    public IntegrationTest(CustomFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task Should_return_weather_forecast_on_http_get()
    {
        var client = _factory.CreateClient();

        var response = await client.GetAsync("/WeatherForecast");

        response.EnsureSuccessStatusCode();
    }
}
