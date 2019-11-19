using Application.Exceptions;
using Domain.Users;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Boundaries.CreateUser
{
    public interface IUseCase : IUseCase<CreateUserInput>  { }


    public sealed class CreateUserInput : IUseCaseInput
    {
        public User User { get; }
        public CreateUserInput(ShortName name, Email email, Password password)
        {
            User = new User(name, email, password);                        
        }
    }
}
