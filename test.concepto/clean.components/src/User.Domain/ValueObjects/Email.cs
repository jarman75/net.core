using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace User.Domain.ValueObjects
{
    public class Email : ValueObject
    {
        private readonly string _text;

        public Email(string text)
        {
            var hasCorrectFormat = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            if (!hasCorrectFormat.IsMatch(text)) throw new EmailValidationException("The 'Email' format is not valid.");

            _text = text;
        }

        public override string ToString() => _text;

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return _text;
        }
    }
}
