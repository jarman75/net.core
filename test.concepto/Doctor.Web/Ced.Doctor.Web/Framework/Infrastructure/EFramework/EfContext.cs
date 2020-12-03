using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Framework.Infrastructure.EFramework
{
    public class EfContext : DbContext
    {
        public EfContext(DbContextOptions options) : base(options)
        {
        }

        protected EfContext()
        {
        }

        public override int SaveChanges()
        {
            var validationResults = new List<ValidationResult>();

            var entities = from e in ChangeTracker.Entries()
                           where e.State == EntityState.Added || e.State == EntityState.Modified
                           select e.Entity;

            foreach (var entity in entities)
            {
                var validationContext = new ValidationContext(entity);                

                Validator.TryValidateObject(entity, validationContext, validationResults, validateAllProperties: true);
            }

            if (validationResults.Count() > 0) throw new DbEntityValidationException("EF Validation Error.", validationResults);

            return base.SaveChanges();
        }
    }
    
}



