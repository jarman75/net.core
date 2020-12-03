using System;

namespace Domain.Users
{
    [Serializable]
    public class UserNotFoundException : DomainException
    {
        public UserNotFoundException(string message) : base(message)
        {
        }
    }
}
