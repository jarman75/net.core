using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;

namespace ApiSample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        static int _callCount;

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IDiagnosticContext _diagnosticContext;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IDiagnosticContext diagnosticContext)
        {
            _logger = logger;
            _diagnosticContext = diagnosticContext;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            _logger.LogInformation("Hello, world!");

            _diagnosticContext.Set("IndexCallCount", Interlocked.Increment(ref _callCount));

            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
