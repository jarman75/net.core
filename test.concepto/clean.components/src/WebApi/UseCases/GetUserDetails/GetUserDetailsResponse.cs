using System;
using System.ComponentModel.DataAnnotations;

namespace WebApi.UseCases.GetUserDetails
{
    /// <summary>
    /// The User Details
    /// </summary>
    public sealed class GetUserDetailsResponse
    {
        /// <summary>
        /// Gets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        [Required]
        public Guid UserId { get; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [Required]
        public string Name { get; }

        /// <summary>
        /// Gets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        [Required]
        public string Email { get; }

        public GetUserDetailsResponse(Guid userId, string name, string email)
        {
            UserId = userId;
            Name = name;
            Email = email;
        }
    }
}
