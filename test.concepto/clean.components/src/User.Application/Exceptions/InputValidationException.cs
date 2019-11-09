using System;
using User.Domain;

namespace User.Application.Exceptions
{
    [Serializable]
    public class InputValidationException : DomainException
    {
        public InputValidationException(string message) : base(message)
        {
        }
    }
}
