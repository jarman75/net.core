using Application.Exceptions;
using System;

namespace Application.UseCases.GetUserDetails
{
    public interface IUseCase : IUseCase<GetUserDetailsInput> { }
    public sealed class GetUserDetailsInput : IUseCaseInput
    {
        public Guid UserId { get; }
        public GetUserDetailsInput(Guid userId)
        {
            if (userId == Guid.Empty)
            {
                throw new InputValidationException($"{nameof(userId)} cannot be empty.");
            }

            UserId = userId;
        }
    }
}
