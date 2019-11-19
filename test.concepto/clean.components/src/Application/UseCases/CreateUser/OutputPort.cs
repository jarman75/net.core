using System;

namespace Application.UseCases.CreateUser
{
    public interface IOutputPort :
        IOutputPortStandard<CreateUserOutput>
    { }

    public sealed class CreateUserOutput : IUseCaseOutput
    {
        public Guid UserId { get; }

        public CreateUserOutput(Guid userId)
        {
            UserId = userId;
        }
    }


}
