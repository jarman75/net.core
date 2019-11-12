using System;
using System.Collections.Generic;
using Domain.Users;
using Domain.ValueObjects;

namespace Application.Boundaries.GetUserDetails
{
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
