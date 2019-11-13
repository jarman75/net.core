using System;
using System.ComponentModel.DataAnnotations;

namespace WebApi.UseCases.GetUserDetails
{
    /// <summary>
    /// The Get User Details Request
    /// </summary>
    public sealed class GetUserDetailsRequest
    {
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        [Required]
        public Guid UserId { get; set; }
    }
}
