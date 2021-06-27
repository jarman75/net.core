using System.Threading.Tasks;

namespace PostService.Models
{
    public interface IPostRepository
    {
        Task<Post> GetPostAsync(string postId);
        Task<Post> UpdatePostAsync(Post post);
        
    }
}
