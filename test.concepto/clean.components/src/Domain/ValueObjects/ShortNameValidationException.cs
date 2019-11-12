using System;
using System.Runtime.Serialization;

namespace Domain.ValueObjects
{
    [Serializable]
    public class ShortNameValidationException : Exception
    {
        public ShortNameValidationException()
        {
        }

        public ShortNameValidationException(string message) : base(message)
        {
        }

        public ShortNameValidationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ShortNameValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}