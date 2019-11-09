using System;
using System.Threading.Tasks;
using User.Domain.Users;

namespace User.Application.Repositories
{
    public interface IUserRepository
    {
        Task<IUser> Get(Guid id);
        Task Add(IUser user);
        Task Update(IUser user);
    }
}
