using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Domain.ValueObjects
{
    public class Password : ValueObject
    {

        private readonly string _text;

        public Password(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) throw new PasswordValidationException("The 'Password' is required.");

            _text = text;

            var hasMinimumChars = new Regex(@".{6,}");
            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");

            if (!hasMinimumChars.IsMatch(text)) throw new PasswordValidationException("The 'Password' must include at least six chars.");
            if (!hasNumber.IsMatch(text)) throw new PasswordValidationException("The 'Password' must include at least one number.");
            if (!hasUpperChar.IsMatch(text)) throw new PasswordValidationException("The 'Password' must include at least one upperChar.");

        }

        public override string ToString() => _text;

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return _text;
        }
    }

}
