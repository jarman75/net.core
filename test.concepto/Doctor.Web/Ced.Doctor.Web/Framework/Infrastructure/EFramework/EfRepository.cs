using Microsoft.EntityFrameworkCore;
using Framework.Core;
using Framework.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace Framework.Infrastructure.EFramework
{
    /// <summary>
    /// "There's some repetition here - couldn't we have some the sync methods call the async?"
    /// https://blogs.msdn.microsoft.com/pfxteam/2012/04/13/should-i-expose-synchronous-wrappers-for-asynchronous-methods/
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>    
    public class EfRepository<TEntity> : IRepository<TEntity>, IAsyncRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EfRepository{TEntity}"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public EfRepository(EfContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// The database context
        /// </summary>
        protected readonly EfContext _dbContext;

        private EntityStatus _status;

        /// <summary>
        /// Gets the last status.
        /// </summary>
        /// <returns></returns>
        public EntityStatus GetLastStatus()
        {
            return _status ?? new EntityStatus();
        }

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public virtual TEntity GetById(int id)
        {
            return _dbContext.Set<TEntity>().Find(id);
        }

        /// <summary>
        /// Gets the single by spec.
        /// </summary>
        /// <param name="spec">The spec.</param>
        /// <returns></returns>
        public TEntity GetSingleBySpec(ISpecification<TEntity> spec)
        {
            return List(spec).FirstOrDefault();
        }

        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public virtual Task<TEntity> GetByIdAsync(int id)
        {
            return _dbContext.Set<TEntity>().FindAsync(id);
        }

        /// <summary>
        /// Lists all.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TEntity> ListAll()
        {
            return _dbContext.Set<TEntity>().AsEnumerable();
        }

        /// <summary>
        /// Lists all asynchronous.
        /// </summary>
        /// <returns></returns>
        public Task<List<TEntity>> ListAllAsync()
        {
            return _dbContext.Set<TEntity>().ToListAsync();
        }

        /// <summary>
        /// Lists the specified spec.
        /// </summary>
        /// <param name="spec">The spec.</param>
        /// <returns></returns>
        public IEnumerable<TEntity> List(ISpecification<TEntity> spec)
        {
            // fetch a Queryable that includes all expression-based includes
            var queryableResultWithIncludes = spec.Includes
                .Aggregate(_dbContext.Set<TEntity>().AsQueryable(),
                    (current, include) => current.Include(include));

            // modify the IQueryable to include any string-based include statements
            var secondaryResult = spec.IncludeStrings
                .Aggregate(queryableResultWithIncludes,
                    (current, include) => current.Include(include));

            // return the result of the query using the specification's criteria expression
            return secondaryResult
                            .Where(spec.Criteria)
                            .AsEnumerable();
        }

        /// <summary>
        /// Lists the asynchronous.
        /// </summary>
        /// <param name="spec">The spec.</param>
        /// <returns></returns>
        public Task<List<TEntity>> ListAsync(ISpecification<TEntity> spec)
        {
            // fetch a Queryable that includes all expression-based includes
            var queryableResultWithIncludes = spec.Includes
                .Aggregate(_dbContext.Set<TEntity>().AsQueryable(),
                    (current, include) => current.Include(include));

            // modify the IQueryable to include any string-based include statements
            var secondaryResult = spec.IncludeStrings
                .Aggregate(queryableResultWithIncludes,
                    (current, include) => current.Include(include));

            // return the result of the query using the specification's criteria expression
            return secondaryResult
                            .Where(spec.Criteria)
                            .ToListAsync();
        }

        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public TEntity Add(TEntity entity)
        {
            var result =  this.Add(entity, true);
            return result.Item2;
        }

        /// <summary>
        /// Adds the asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public async Task<TEntity> AddAsync(TEntity entity)
        {
          

            try
            {
                _dbContext
                .Set<TEntity>()
                .Add(entity);

                await _dbContext.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (DbEntityValidationException ex)
            {
                _status = new EntityStatus { Id = 0 };
                _status.SetErrors(ex?.EntityValidationErrors);

                throw;
            }
            catch (DbUpdateException ex)
            {
                var decodedErrors = TryDecodeDbUpdateException(ex);
                if (decodedErrors != null)
                {
                    _status = new EntityStatus { Id = 0 };
                    _status.SetErrors(decodedErrors);
                }

                throw;
            }

            return entity;
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Update(TEntity entity)
        {
            this.Update(entity, true);
        }

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public async Task UpdateAsync(TEntity entity)
        {
            

            try
            {
                _dbContext.Entry(entity).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (DbEntityValidationException ex)
            {
                _status = new EntityStatus { Id = 0 };
                _status.SetErrors(ex?.EntityValidationErrors);

                throw;
            }
            catch (DbUpdateException ex)
            {
                var decodedErrors = TryDecodeDbUpdateException(ex);
                if (decodedErrors != null)
                {
                    _status = new EntityStatus { Id = 0 };
                    _status.SetErrors(decodedErrors);
                }

                throw;
            }
        }

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Delete(TEntity entity)
        {
            this.Delete(entity, true);
        }

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public async Task DeleteAsync(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Attaches the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public TEntity Attach(TEntity entity)
        {
            _dbContext.Set<TEntity>().Attach(entity);
            return entity;
        }

        /// <summary>
        /// Attaches the specified entities.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <returns></returns>
        public List<TEntity> Attach(List<TEntity> entities)
        {
            foreach (var item in entities)
            {
                _dbContext.Set<TEntity>().Attach(item);
            }

            return entities;
        }

        /// <summary>
        /// Updates the range.
        /// </summary>
        /// <param name="entities">The entities.</param>
        public void UpdateRange(List<TEntity> entities)
        {
            this.UpdateRange(entities, true);
        }

        /// <summary>
        /// Adds the range.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <returns></returns>
        public List<TEntity> AddRange(List<TEntity> entities)
        {
            var result = this.AddRange(entities, true);
            return result.Item2;
        }

        #region Extension Methods

        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="omitExceptions">if set to <c>true</c> [omit exceptions].</param>
        /// <returns>Item1=operation result, Item2=entity</returns>
        public Tuple<bool,TEntity> Add(TEntity entity, bool omitExceptions)
        {
            Tuple<bool, TEntity> result = new Tuple<bool, TEntity>(false, entity);
            
            try
            {
                _dbContext.Set<TEntity>().Add(entity);
                _dbContext.SaveChanges();
                result = new Tuple<bool, TEntity>(true, entity);

            }
            catch (DbEntityValidationException ex)
            {

                if (!omitExceptions) throw;
                _status = new EntityStatus { Id = 0 };
                _status.SetErrors(ex?.EntityValidationErrors);
                
            }
            catch (DbUpdateException ex)
            {
                if (!omitExceptions) throw;
                var decodedErrors = TryDecodeDbUpdateException(ex);
                if (decodedErrors != null)
                {
                    _status = new EntityStatus { Id = 0 };
                    _status.SetErrors(decodedErrors);
                }                
            }
            catch (Exception ex)
            {
                if (!omitExceptions) throw;
                _status = new EntityStatus { Id = 0 };
                _status.SetErrors(new List<ValidationResult> { new ValidationResult(ex.Message) });
            }

            return result;

        }

        /// <summary>
        /// Adds the range.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <param name="omitExceptions">if set to <c>true</c> [omit exceptions].</param>
        /// <returns>Item1=operation result, Item2=List<typeparamref name="TEntity"/></returns>
        public Tuple<bool,List<TEntity>> AddRange(List<TEntity> entities, bool omitExceptions)
        {
            
            Tuple<bool, List<TEntity>> result = new Tuple<bool, List<TEntity>>(false, entities);                       

            try
            {
                _dbContext.Set<TEntity>().AddRange(entities);
                _dbContext.SaveChanges();
                result = new Tuple<bool, List<TEntity>>(true, entities);
            }
            catch (DbEntityValidationException ex)
            {
                if (!omitExceptions) throw;
                _status = new EntityStatus();
                _status.SetErrors(ex?.EntityValidationErrors);                
            }
            catch (DbUpdateException ex)
            {
                if (!omitExceptions) throw;
                var decodedErrors = TryDecodeDbUpdateException(ex);
                if (decodedErrors != null)
                {   _status = new EntityStatus();
                    _status.SetErrors(decodedErrors);
                }
            }
            catch (Exception ex)
            {
                if (!omitExceptions) throw;
                _status = new EntityStatus();
                _status.SetErrors(new List<ValidationResult> { new ValidationResult(ex.Message) });
            }

            return result;
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="omitExceptions">if set to <c>true</c> [omit exceptions].</param>
        /// <returns>operation result</returns>
        public bool Update(TEntity entity, bool omitExceptions)
        {
            bool result = false;

            try
            {
                _dbContext.Entry(entity).State = EntityState.Modified;
                _dbContext.SaveChanges();
                result = true;
            }
            catch (DbEntityValidationException ex)
            {
                if (!omitExceptions) throw;
                _status = new EntityStatus { Id = 0 };
                _status.SetErrors(ex?.EntityValidationErrors);                
            }
            catch (DbUpdateException ex)
            {
                if (!omitExceptions) throw;
                var decodedErrors = TryDecodeDbUpdateException(ex);
                if (decodedErrors != null)
                {
                    _status = new EntityStatus { Id = 0 };
                    _status.SetErrors(decodedErrors);
                }
            }
            catch (Exception ex)
            {
                if (!omitExceptions) throw;
                _status = new EntityStatus { Id = 0 };
                _status.SetErrors(new List<ValidationResult> { new ValidationResult(ex.Message) });
            }

            return result;
        }

        /// <summary>
        /// Updates the range.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <param name="omitExceptions">if set to <c>true</c> [omit exceptions].</param>
        /// <returns>Item1=operation result, Item2=List<typeparamref name="TEntity"/></returns>
        public Tuple<bool,List<TEntity>> UpdateRange(List<TEntity> entities, bool omitExceptions)
        {

            int id = 0;
            Tuple<bool, List<TEntity>> result = new Tuple<bool, List<TEntity>>(false, entities);            

            try
            {
                foreach (var item in entities)
                {
                    id = 0;
                    _dbContext.Entry(item).State = EntityState.Modified;
                }

                _dbContext.SaveChanges();
                result = new Tuple<bool, List<TEntity>>(true, entities);
            }
            catch (DbEntityValidationException ex)
            {
                if (!omitExceptions) throw;
                _status = new EntityStatus { Id = id };
                _status.SetErrors(ex?.EntityValidationErrors);
            }
            catch (DbUpdateException ex)
            {
                if (!omitExceptions) throw;
                var decodedErrors = TryDecodeDbUpdateException(ex);
                if (decodedErrors != null)
                {
                    _status = new EntityStatus { Id = id };
                    _status.SetErrors(decodedErrors);
                }
            }
            catch(Exception ex)
            {
                if (!omitExceptions) throw;
                _status = new EntityStatus { Id = id };
                _status.SetErrors(new List<ValidationResult> { new ValidationResult(ex.Message) });
            }

            return result;
        }

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="omitExceptions">if set to <c>true</c> [omit exceptions].</param>
        /// <returns>true is operation correct</returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool Delete(TEntity entity, bool omitExceptions)
        {
            bool result = false;
            try
            {
                _dbContext.Set<TEntity>().Remove(entity);
                _dbContext.SaveChanges();
                result = true;
            }
            catch (Exception ex)
            {
                if (!omitExceptions) throw;
                _status = new EntityStatus { Id = 0 };
                _status.SetErrors(new List<ValidationResult> { new ValidationResult(ex.Message) });
            }
            return result;
        }
        #endregion

        #region "Update Exceptions"

        private readonly Dictionary<int, string> _sqlErrorTextDict =
            new Dictionary<int, string>
            {
                { 547, "This operation failed because another data entry uses this entry."},
                {2601, "One of the properties is marked as Unique index and there is already an entry with that value."}
            };

        private IEnumerable<ValidationResult> TryDecodeDbUpdateException(DbUpdateException dbUpdateException)
        {
            
            if (!(dbUpdateException.InnerException is System.Data.EvaluateException)
                || !(dbUpdateException.InnerException.InnerException is System.Data.SqlClient.SqlException))
            {
                return new List<ValidationResult>();
            }

            var sqlException =
                (System.Data.SqlClient.SqlException)dbUpdateException.InnerException.InnerException;
            var result = new List<ValidationResult>();
            for (int i = 0; i < sqlException.Errors.Count; i++)
            {
                var errorNum = sqlException.Errors[i].Number;
                if (_sqlErrorTextDict.TryGetValue(errorNum, out string errorText))
                    result.Add(new ValidationResult(errorText));
            }
            return result.Count > 0 ? result : new List<ValidationResult>();
        }        
        #endregion

    }
}
