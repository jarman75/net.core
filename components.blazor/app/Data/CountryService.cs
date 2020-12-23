using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace app.Data
{
    public class Translations 
    {
        
        public string Es {get; set;}        

    }
    public class Country {
        public string Name {get; set;}
        public string Alpha2Code {get; set;}
        public string Alpha3Code {get; set;}
        public string Capital {get; set;}
        public Translations Translations {get; set;}
         
    }

    public class CountryService : ICountryService
    {
        private readonly HttpClient httpClient;

        public CountryService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IEnumerable<Country>> GetCountrys(string countrySearch)
        {
            try 
            {
                return await httpClient.GetFromJsonAsync<IEnumerable<Country>>($"rest/v2/name/{countrySearch}");                
            }
            catch
            {
                return new List<Country>();
            }
            
            
            
            
        }
    }

    public interface ICountryService
    {
        Task<IEnumerable<Country>> GetCountrys(string countrySearch);
    }
}