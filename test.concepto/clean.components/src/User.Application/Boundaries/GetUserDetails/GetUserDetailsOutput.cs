using System;
using System.Collections.Generic;
using User.Domain.Users;
using User.Domain.ValueObjects;

namespace User.Application.Boundaries.GetUserDetails
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
            var userEntity = (User.Domain.Users.User)user;
            UserId = userEntity.Id;
            UserName = userEntity.Name;
            Email = userEntity.Email;
            RequiredChangePassword = userEntity.RequiredChangePassword;
            Roles = userEntity.Roles.GetRoleIds();
        }
    }
}
