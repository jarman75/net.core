using Domain;
using System;

namespace Application.Exceptions
{
    [Serializable]
    public class InputValidationException : DomainException
    {
        public InputValidationException(string message) : base(message)
        {
        }
    }
}
