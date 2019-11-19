using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Boundaries.CreateUser
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
