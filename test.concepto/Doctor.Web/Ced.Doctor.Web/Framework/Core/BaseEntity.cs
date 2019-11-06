using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Framework.Core
{
    /// <summary>
    /// BaseEntity. Entity objects are used to persist data through their repository.
    /// </summary>
    public class BaseEntity
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public virtual int Id { get; set; }

        /// <summary>
        /// Gets the validation status.
        /// </summary>
        /// <returns></returns>
        public EntityStatus GetValidationStatus()
        {
            EntityStatus status = new EntityStatus(this.Id);

            try
            {
                var context = new ValidationContext(this, null, null);
                var vResult = new List<ValidationResult>();

                var isValid = Validator.TryValidateObject(this, context, vResult, true);
                if (!isValid)  status.SetErrors(vResult);
            }
            catch
            {
                status = new EntityStatus(this.Id);
            }

            return status;
        }
    }
}
