using System;
using System.Runtime.Serialization;

namespace Domain.ValueObjects
{
    [Serializable]
    internal class PersonNameShouldNotBeEmptyException : DomainException
    {
        public PersonNameShouldNotBeEmptyException(string message) : base(message)
        {
        }
    }
}