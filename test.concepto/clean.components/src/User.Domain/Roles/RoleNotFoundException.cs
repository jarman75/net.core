using System;

namespace User.Domain.Roles
{
    [Serializable]
    public class RoleNotFoundException : DomainException
    {
        public RoleNotFoundException(string message) : base(message)
        {
        }
    }
}
