using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;


namespace AdminApp.Data
{
    public class TokenContainer
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TokenContainer(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task AddRequestHeaders(HttpClient httpClient)
        {
            
            var accessToken = await _httpContextAccessor.HttpContext.GetTokenAsync("access_token");            
            var identityToken = await _httpContextAccessor.HttpContext.GetTokenAsync("id_token");
            
            var data = _httpContextAccessor.HttpContext.User;

            httpClient.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));            
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        }
    }
}