using System.Threading.Tasks;

namespace User.Application.Services
{
    public interface IUnitOfWork
    {
        Task<int> Save();
    }
}
