namespace Domain.ValueObjects
{
    public readonly struct PersonName
    {
        private readonly string _text;

        public PersonName(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                throw new PersonNameShouldNotBeEmptyException($"The {nameof(text)} field is required.");
            }

            _text = text;
        }

        public override string ToString()
        {
            return _text;
        }
    }
}