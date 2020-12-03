using System.Threading.Tasks;

namespace Application.UseCases
{
    public interface IUseCase<in TUseCaseInput> where TUseCaseInput : IUseCaseInput
    {
        Task Execute(TUseCaseInput input);
    }
}
