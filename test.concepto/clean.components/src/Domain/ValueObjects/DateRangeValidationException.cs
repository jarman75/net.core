using Domain;

public sealed class DateRangeValidationException : DomainException
    {
        public DateRangeValidationException(string message) : base(message)
        {
        }
    }