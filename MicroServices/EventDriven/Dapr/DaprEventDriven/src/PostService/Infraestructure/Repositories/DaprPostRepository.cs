using Dapr.Client;
using Microsoft.Extensions.Logging;
using PostService.Models;
using System.Threading.Tasks;

namespace PostService.Infraestructure.Repositories
{
    public class DaprPostRepository : IPostRepository
    {
        private const string StoreName = "statestore";

        private readonly ILogger<DaprPostRepository> _logger;
        private readonly DaprClient _dapr;

        public DaprPostRepository(ILogger<DaprPostRepository> logger, DaprClient dapr)
        {
            _logger = logger;
            _dapr = dapr;
        }
        public async Task<Post> GetPostAsync(string postId)
        {
            return await _dapr.GetStateAsync<Post>(StoreName, postId);
        }
        public async Task<Post> UpdatePostAsync(Post post)
        {
            var state = await _dapr.GetStateEntryAsync<Post>(StoreName, post.Id);
            state.Value = post;

            await state.SaveAsync();

            _logger.LogInformation("Post item persisted succesfully.");

            return await GetPostAsync(post.Id);
        }
    }
}
