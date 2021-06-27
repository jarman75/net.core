using Dapr.Client;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using UserService.Models;

namespace UserService.Infraestructure.Repositories
{
    public class DaprUserRepository : IUserRepository
    {
        private const string StoreName = "statestore";

        private readonly ILogger<DaprUserRepository> _logger;
        private readonly DaprClient _dapr;

        public DaprUserRepository(ILogger<DaprUserRepository> logger, DaprClient dapr)
        {
            _logger = logger;
            _dapr = dapr;
        }

        public async Task DeleteUserAsync(string userId)
        {
            await _dapr.DeleteStateAsync(StoreName, userId);
        }

        public async Task<User> GetUserAsync(string userId)
        {
            return await _dapr.GetStateAsync<User>(StoreName, userId);
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            var state = await _dapr.GetStateEntryAsync<User>(StoreName, user.Id);
            state.Value = user;

            await state.SaveAsync();

            _logger.LogInformation("User item persisted succesfully.");

            return await GetUserAsync(user.Id);

        }
    }
}
