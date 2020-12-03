namespace Application.UseCases
{
    public interface IOutputPortStandard<in TUseCaseOutput> where TUseCaseOutput : IUseCaseOutput
    {
        void Standard(TUseCaseOutput output);
    }
}
