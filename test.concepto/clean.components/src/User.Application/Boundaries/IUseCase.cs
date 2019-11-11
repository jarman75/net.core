using System.Threading.Tasks;

namespace User.Application.Boundaries
{
    public interface IUseCase<in TUseCaseInput> where TUseCaseInput : IUseCaseInput
    {
        Task Execute(TUseCaseInput input);
    }
}
