namespace Infrastructure.Exceptions
{
    public class UserDataException : Domain.DomainException
    {
        public UserDataException(string message) : base(message)
        {
        }
    }
}
