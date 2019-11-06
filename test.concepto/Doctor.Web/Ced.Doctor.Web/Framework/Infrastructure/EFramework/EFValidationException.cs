using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace Framework.Infrastructure.EFramework
{
    public class DbEntityValidationException : ValidationException
    {
        
        public DbEntityValidationException()
        {
        }

        public DbEntityValidationException(string message) : base(message)
        {
        }

        public DbEntityValidationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public DbEntityValidationException(ValidationResult validationResult, ValidationAttribute validatingAttribute, object value) : base(validationResult, validatingAttribute, value)
        {
        }

        public DbEntityValidationException(string errorMessage, ValidationAttribute validatingAttribute, object value) : base(errorMessage, validatingAttribute, value)
        {
        }

        protected DbEntityValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public DbEntityValidationException(string message, ICollection<ValidationResult> validationResult): base(message)
        {
            EntityValidationErrors = validationResult;
        }

        public ICollection<ValidationResult> EntityValidationErrors { get; private set; }
    }
}
