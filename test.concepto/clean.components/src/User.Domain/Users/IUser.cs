using User.Domain.Roles;

namespace User.Domain.Users
{
    public interface IUser : IAggregateRoot
    {
        void ResetPassword();

        void Register(IRole role);
    }
}
