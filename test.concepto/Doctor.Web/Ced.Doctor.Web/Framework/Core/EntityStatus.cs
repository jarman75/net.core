using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Framework.Core
{
    /// <summary>
    /// Entity Framework Status
    /// </summary>
    public class EntityStatus
    {
        /// <summary>
        /// The errores
        /// </summary>
        private List<ValidationResult> _errors;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityStatus"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public EntityStatus(int id)
        {
            Id = id;
        }

        public EntityStatus() { }

        /// <summary>
        /// Returns true if ... is valid.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </value>
        public bool IsValid { get { return _errors == null; } }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        /// TODO Edit XML Comment Template for Id
        public int Id { get; set; }

        /// <summary>
        /// Gets the ef errors.
        /// </summary>
        /// <value>
        /// The ef errors.
        /// </value>
        public IReadOnlyList<ValidationResult> EfErrors
        {
            get { return _errors ?? new List<ValidationResult>(); }
        }
        
        /// <summary>
        /// Sets the errors.
        /// </summary>
        /// <param name="errors">The errors.</param>
        /// <returns></returns>
        public EntityStatus SetErrors(IEnumerable<ValidationResult> errors)
        {
            _errors = errors.ToList();
            return this;
        }
    }
}
