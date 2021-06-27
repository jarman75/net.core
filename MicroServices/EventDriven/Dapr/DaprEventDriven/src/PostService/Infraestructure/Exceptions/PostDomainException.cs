using System;

namespace PostService.Infraestructure.Exceptions
{
    /// <summary>
    /// Exception type for app exceptions
    /// </summary>
    public class PostDomainException : Exception
    {
        public PostDomainException()
        {
        }

        public PostDomainException(string message) : base(message)
        {
        }

        public PostDomainException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
