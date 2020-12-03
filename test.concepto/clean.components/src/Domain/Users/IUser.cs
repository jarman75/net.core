using Domain.Roles;

namespace Domain.Users
{
    public interface IUser : IAggregateRoot
    {
        void ResetPassword();

        void Register(IRole role);
    }
}
