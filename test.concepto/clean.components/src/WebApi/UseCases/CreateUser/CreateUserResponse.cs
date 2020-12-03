using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.UseCases.CreateUser
{
    /// <summary>
    /// The New User Response
    /// </summary>
    public sealed class CreateUserResponse
    {
        /// <summary>
        /// Gets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public Guid UserId { get; }

        public CreateUserResponse(Guid userId)
        {
            UserId = userId;
        }
    }
}
