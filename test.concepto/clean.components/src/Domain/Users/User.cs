using System;
using Domain.Roles;
using Domain.ValueObjects;

namespace Domain.Users
{
    public class User : IUser
    {
        public Guid Id { get; protected set; }
        public ShortName Name { get; protected set; }
        public Email Email { get; protected set; }
        public RoleCollection Roles { get; protected set; }
        public Password Password { get; protected set; }
        public bool RequiredChangePassword { get; protected set; }

        public User()
        {
            Roles = new RoleCollection();
        }

        public void ResetPassword()
        {
            RequiredChangePassword = true;
        }

        public void Register(IRole role)
        {
            Roles ??= new RoleCollection();
            Roles.Add(role.Id);
        }
    }

}