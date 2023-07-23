using Example.Api.Data;
using Microsoft.AspNetCore.Mvc;

namespace Example.Api.Controllers;
[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
   
    private readonly ILogger<WeatherForecastController> _logger;
    private readonly AppDbContext _dbContext;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, AppDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return _dbContext.WeatherForecasts.ToArray();        
        
    }
    //get a weather forecast by id
    [HttpGet("{id}", Name = "GetWeatherForecastById")]
    public async Task<IActionResult> GetWeatherForecastById(int id)
    {
        var weatherForecast = await _dbContext.WeatherForecasts.FindAsync(id);
        if (weatherForecast == null)
        {
            return NotFound();
        }
        return Ok(weatherForecast);
    }

    //create a new weather forecast
    [HttpPost(Name = "CreateWeatherForecast")]
    public async Task<IActionResult> CreateWeatherForecast([FromBody] WeatherForecast weatherForecast)
    {
        _dbContext.WeatherForecasts.Add(weatherForecast);
        await _dbContext.SaveChangesAsync();
        return CreatedAtRoute("GetWeatherForecastById", new { id = weatherForecast.Id }, weatherForecast);
    }
  
}
