using Domain;
using Domain.Roles;
using Domain.Users;
using Domain.ValueObjects;
using System;

namespace Infrastructure.EntityFrameworkDataAccess
{
    public sealed class EntityFactory : IEntityFactory
    {
        public IRole NewRole(IUser user)
        {
            return new Role();
        }

        public IUser NewUser(ShortName name, Email email, Password password)
        {
            return new User(name, email, password);
        }
    }
}
