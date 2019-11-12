using System;
using Application.Exceptions;

namespace Application.Boundaries.GetUserDetails
{
    public sealed class GetUserDetailsInput : IUseCaseInput
    {
        public Guid UserId { get; }
        public GetUserDetailsInput(Guid userId)
        {
            if (userId == Guid.Empty)
            {
                throw new InputValidationException($"{nameof(userId)} cannot be empty.");
            }
        }
    }
}
