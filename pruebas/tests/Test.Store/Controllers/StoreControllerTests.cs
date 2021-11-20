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
            Assert.AreEqual("{\"losses\":2.92,\"benefits\":1628.58}", responseString);

        }
        [Test]
        public async Task SetStocksPrice()
        {
            //Act 
            var response = await _client.PatchAsync("/Store/SetStocksPrice", null);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert            
            Assert.AreEqual("6 items have been updated.", responseString);

        }
    }
}
