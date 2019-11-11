using System;

namespace User.Domain.ValueObjects
{
    [Serializable]
    public class PasswordValidationException : DomainException
    {
        public PasswordValidationException(string message) : base(message)
        {
        }

    }
}