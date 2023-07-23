using Example.Api;
using Example.Api.Data;
using Integration.Test.Setup;
using System.Net;
using System.Net.Http.Json;

namespace Integration.Test;
public class IntegrationFactoryTests : IClassFixture<IntegrationTestFactory<Program, AppDbContext>>
{
    private readonly IntegrationTestFactory<Program, AppDbContext> _factory;

    public IntegrationFactoryTests(IntegrationTestFactory<Program, AppDbContext> factory) => _factory = factory;

    [Fact]
    public async Task Get()
    {
        var client = _factory.CreateClient();

        var response = await client.GetAsync("/weatherforecast");

        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task GetById()
    {
        var client = _factory.CreateClient();
        //create a new weather forecast
        var weatherForecast = new WeatherForecast
        {           
            Date = new DateOnly(2022, 6, 1),
            TemperatureC = 25,
            Summary = "Sunny"
        };
        var createResponse = await client.PostAsJsonAsync("/weatherforecast", weatherForecast);


        var response = await client.GetAsync("/weatherforecast/1");

        response.EnsureSuccessStatusCode();
    }

    //not found weather forecast when id is not found
    [Fact]
    public async Task GetById_NotFound()
    {
        var client = _factory.CreateClient();

        var response = await client.GetAsync("/weatherforecast/0");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
    
    [Fact]
    public async Task Create()
    {
        var client = _factory.CreateClient();
        //create a new weather forecast
        var weatherForecast = new WeatherForecast
        {
            Date = new DateOnly(2022,6,1),
            TemperatureC = 25,
            Summary = "Sunny"
        };
        var response = await client.PostAsJsonAsync("/weatherforecast", weatherForecast);

        response.EnsureSuccessStatusCode();
    }
}
