namespace Framework.Core.Interfaces
{
    public interface IRuleResult
    {
        bool IsSucess { get; set; }

        void Execute();
    }
}
