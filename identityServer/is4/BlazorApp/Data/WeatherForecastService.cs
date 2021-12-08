using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlazorApp.Data
{
    public class WeatherForecastService
    {
        private readonly IHttpClientFactory _clientFactory;
        const string apiClient = "api1";
        const string apiResource = "WeatherForecast";

        public WeatherForecastService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<WeatherForecast[]> GetForecastAsync(DateTime startDate)
        {

            IEnumerable<WeatherForecast> result = new List<WeatherForecast>();

            try
            {
                var client = _clientFactory.CreateClient(apiClient);

                var request = new HttpRequestMessage(HttpMethod.Get, apiResource);
                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var responseStream = await response.Content.ReadAsStreamAsync();
                    result = await JsonSerializer.DeserializeAsync
                            <IEnumerable<WeatherForecast>>(responseStream);
                }
            }
            catch (Exception ex)
            {
                var x = $"{ex.Message}";
                throw;
            }
            
            

            return result.ToArray();
        }
    }
}
