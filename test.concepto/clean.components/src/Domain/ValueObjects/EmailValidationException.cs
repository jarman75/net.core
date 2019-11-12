using System;

namespace Domain.ValueObjects
{
    [Serializable]
    public class EmailValidationException : DomainException
    {
        public EmailValidationException(string message) : base(message)
        {
        }

    }
}