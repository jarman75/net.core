using User.Domain.Roles;
using User.Domain.Users;
using User.Domain.ValueObjects;


namespace User.Domain
{
    public interface IEntityFactory
    {
        IUser NewUser(ShortName name, Email email, Password password);
        IRole NewRole(IUser user);
    }
}
