using System.Collections.Generic;

namespace User.Domain.ValueObjects
{
    public class ShortName : ValueObject
    {
        private readonly string _text;

        public ShortName(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) throw new ShortNameValidationException("The 'Name' is required.");

            if (text.Length > 15) throw new ShortNameValidationException("The 'Name' supports a maximum of 15 characters.");

            _text = text;
        }


        public override string ToString() => _text;

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return _text;
        }
    }
}
