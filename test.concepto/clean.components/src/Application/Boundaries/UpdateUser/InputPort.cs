using Domain.Users;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Boundaries.UpdateUser
{
    public interface IUseCase : IUseCase<UpdateUserInput> { }

    public sealed class UpdateUserInput : IUseCaseInput
    {
        public User User { get;  }

        public UpdateUserInput(Guid id, ShortName name, Email email)
        {
            User = new User(id, name, email);
        }
    }
}
