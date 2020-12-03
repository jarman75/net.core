using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Framework.Core
{
    public class RequestValidationException : Exception
    {
        public RequestValidationException() : base("Request validation exception.")
        {
        }

        public RequestValidationException(string message) : base(message)
        {
        }

        public RequestValidationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected RequestValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException() : base("Entity not found exception.")
        {            
        }

        public EntityNotFoundException(string message) : base(message)
        {
        }

        public EntityNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected EntityNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    public class InvalidServiceOperation : Exception
    {
        public InvalidServiceOperation() : base("Invalid service operation exception.")
        {
        }

        public InvalidServiceOperation(string message) : base(message)
        {
        }

        public InvalidServiceOperation(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidServiceOperation(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

}
