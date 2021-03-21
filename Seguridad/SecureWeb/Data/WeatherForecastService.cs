using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace SecureWeb.Data
{
    public class WeatherForecastService
    {
        //private static readonly string[] Summaries = new[]
        //{
        //    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        //};

        //public Task<WeatherForecast[]> GetForecastAsync(DateTime startDate)
        //{
        //    var rng = new Random();
        //    return Task.FromResult(Enumerable.Range(1, 5).Select(index => new WeatherForecast
        //    {
        //        Date = startDate.AddDays(index),
        //        TemperatureC = rng.Next(-20, 55),
        //        Summary = Summaries[rng.Next(Summaries.Length)]
        //    }).ToArray());
        //}

        private readonly IHttpClientFactory _clientFactory;        

        public WeatherForecastService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;            
        }
        public async Task<WeatherForecast[]> GetForecastAsync(DateTime startDate)
        {

            WeatherForecast[] result;

            var request = new HttpRequestMessage(HttpMethod.Get, $"/WeatherForecast");
            var client = _clientFactory.CreateClient("secure.api");

            var response = client.SendAsync(request).GetAwaiter().GetResult();

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                result = GetListResult(responseStream).GetAwaiter().GetResult().ToArray();

            }
            else
            {
                throw new Exception($"Error api: {response.StatusCode}");
            }

            return result;
        }
        protected virtual async Task<IEnumerable<WeatherForecast>> GetListResult(Stream responseStream)
        {

            IEnumerable<WeatherForecast> result;

            var data = await JsonSerializer.DeserializeAsync<IEnumerable<WeatherForecast>>(responseStream);
            result = data;

            return result;

        }

    }
}
