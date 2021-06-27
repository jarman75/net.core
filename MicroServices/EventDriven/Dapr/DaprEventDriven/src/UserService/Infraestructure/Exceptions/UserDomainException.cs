using System;

namespace UserService.Infraestructure.Exceptions
{
    /// <summary>
    /// Exception type for app exceptions
    /// </summary>
    public class UserDomainException : Exception
    {
        public UserDomainException()
        {
        }

        public UserDomainException(string message) : base(message)
        {
        }

        public UserDomainException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
