using System;
using System.Collections.Generic;
using Domain;

public sealed class DateRange : ValueObject
    {
        public DateTime StartDate {get;}
        public DateTime EndDate {get;}

        public DateRange(DateTime startDate, DateTime? endDate)
        {
            this.StartDate = startDate;
            this.EndDate = endDate ?? DateTime.MaxValue;

            if (this.StartDate > this.EndDate) {
                throw new DateRangeValidationException("The 'startDate' must be less or equal than the 'endDate'.");
            }
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return ($"{StartDate}#{EndDate}");
        }

        public bool Overlap(DateRange other) {
            var result1 = (other.StartDate >= this.StartDate && other.StartDate <= this.EndDate);            
            var result2 = (this.StartDate >= other.StartDate && this.StartDate <= other.EndDate);

            return (result1 || result2);

        }
    }