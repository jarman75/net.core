using Domain.Users;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Boundaries.UpdateUser
{
    public interface IOutputPort : 
        IOutputPortStandard<UpdateUserOutput>, IOutputPortNotFound
    { }

    public sealed class UpdateUserOutput : IUseCaseOutput
    {
        public Guid UserId { get; }
        public ShortName UserName { get; }
        public Email Email { get; }

        public UpdateUserOutput(IUser user)
        {
            var userEntity = (Domain.Users.User)user;
            UserId = userEntity.Id;
            UserName = userEntity.Name;
            Email = userEntity.Email;
        }
    }
}
