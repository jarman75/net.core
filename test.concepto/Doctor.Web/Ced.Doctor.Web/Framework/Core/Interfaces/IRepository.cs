using System;
using System.Collections.Generic;

namespace Framework.Core.Interfaces
{
    /// <summary>
    /// IRepository. Repository to work with persistent entity data.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<TEntity> where TEntity : class
    {

        /// <summary>
        /// Gets the last status.
        /// </summary>
        /// <returns></returns>
        EntityStatus GetLastStatus();

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        TEntity GetById(int id);

        /// <summary>
        /// Gets the single by spec.
        /// </summary>
        /// <param name="spec">The spec.</param>
        /// <returns></returns>
        TEntity GetSingleBySpec(ISpecification<TEntity> spec);

        /// <summary>
        /// Lists all.
        /// </summary>
        /// <returns></returns>
        IEnumerable<TEntity> ListAll();

        /// <summary>
        /// Lists the specified spec.
        /// </summary>
        /// <param name="spec">The spec.</param>
        /// <returns></returns>
        IEnumerable<TEntity> List(ISpecification<TEntity> spec);

        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        TEntity Add(TEntity entity);

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Update(TEntity entity);

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Delete(TEntity entity);
        
        /// <summary>
        /// Adds the range.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <returns></returns>
        List<TEntity> AddRange(List<TEntity> entities);

        /// <summary>
        /// Updates the range.
        /// </summary>
        /// <param name="entities">The entities.</param>
        void UpdateRange(List<TEntity> entities);

        /// <summary>
        /// Attaches the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        TEntity Attach(TEntity entity);

        /// <summary>
        /// Attaches the specified entities.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <returns></returns>
        List<TEntity> Attach(List<TEntity> entities);

        #region Extension Methods

        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="omitExceptions">if set to <c>true</c> [omit exceptions].</param>
        /// <returns>Item1=operation result, Item2=entity</returns>
        Tuple<bool, TEntity> Add(TEntity entity, bool omitExceptions);

        /// <summary>
        /// Adds the range.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <param name="omitExceptions">if set to <c>true</c> [omit exceptions].</param>
        /// <returns>Item1=operation result, Item2=List<typeparamref name="T"/></returns>
        Tuple<bool, List<TEntity>> AddRange(List<TEntity> entities, bool omitExceptions);

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="omitExceptions">if set to <c>true</c> [omit exceptions].</param>
        /// <returns>operation result</returns>
        bool Update(TEntity entity, bool omitExceptions);

        /// <summary>
        /// Updates the range.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <param name="omitExceptions">if set to <c>true</c> [omit exceptions].</param>
        /// <returns>Item1=operation result, Item2=List<typeparamref name="T"/></returns>
        Tuple<bool, List<TEntity>> UpdateRange(List<TEntity> entities, bool omitExceptions);

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="omitExceptions">if set to <c>true</c> [omit exceptions].</param>
        /// <returns></returns>
        bool Delete(TEntity entity, bool omitExceptions);        
        #endregion

    }
}
