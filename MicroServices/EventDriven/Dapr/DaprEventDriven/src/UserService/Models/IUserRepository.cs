using System.Threading.Tasks;

namespace UserService.Models
{
    public interface IUserRepository
    {
        Task<User> GetUserAsync(string userId);
        Task<User> UpdateUserAsync(User user);        
        Task DeleteUserAsync(string userId);
    }
}
