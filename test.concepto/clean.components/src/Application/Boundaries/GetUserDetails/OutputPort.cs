using Domain.Users;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;

namespace Application.Boundaries.GetUserDetails
{
    public interface IOutputPort
        : IOutputPortStandard<GetUserDetailsOutput>, IOutputPortNotFound
    { }


    public sealed class GetUserDetailsOutput : IUseCaseOutput
    {
        public Guid UserId { get; }
        public ShortName UserName { get; }
        public Email Email { get; }
        public bool RequiredChangePassword { get; }
        public IReadOnlyCollection<Guid> Roles { get; }

        public GetUserDetailsOutput(IUser user)
        {
            var userEntity = (Domain.Users.User)user;
            UserId = userEntity.Id;
            UserName = userEntity.Name;
            Email = userEntity.Email;
            RequiredChangePassword = userEntity.RequiredChangePassword;
            Roles = userEntity.Roles.GetRoleIds();
        }
    }
}
