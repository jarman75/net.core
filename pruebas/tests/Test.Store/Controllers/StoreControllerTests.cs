using Api.Store;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using NUnit.Framework;
using System.Net.Http;
using System.Threading.Tasks;


namespace Test.Store.Controllers
{
    public class StoreControllerTests
    {
        private TestServer _server;
        private HttpClient _client;

        [SetUp]
        public void Setup()
        {
            // Arrange
            _server = new TestServer(new WebHostBuilder()
               .UseStartup<Startup>());
            _client = _server.CreateClient();
        }

        [Test]
        public async Task ReturnBalance()
        {
            //Act 
            var response = await _client.GetAsync("/Store/Balance");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert            
            Assert.AreEqual("{\"losses\":1.97,\"benefits\":4592.23}", responseString);

        }
        [Test]
        public async Task SetCostPrices()
        {
            //Act 
            var response = await _client.PatchAsync("/Store/SetCostPrices", null);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert            
            Assert.AreEqual("6 items have been updated.", responseString);

        }
    }
}
