using Domain.Roles;
using Domain.Users;
using Domain.ValueObjects;


namespace Domain
{
    public interface IEntityFactory
    {
        IUser NewUser(ShortName name, Email email, Password password);
        IRole NewRole(IUser user);
    }
}
