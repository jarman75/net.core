using System.Collections.Generic;
using System.Threading.Tasks;

namespace Framework.Core.Interfaces
{

    /// <summary>
    /// IAsyncRepository. Repository to work asynchronously with persistent entity data.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IAsyncRepository<TEntity> where TEntity : class
    {

        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<TEntity> GetByIdAsync(int id);

        /// <summary>
        /// Lists all asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<List<TEntity>> ListAllAsync();

        /// <summary>
        /// Lists the asynchronous.
        /// </summary>
        /// <param name="spec">The spec.</param>
        /// <returns></returns>
        Task<List<TEntity>> ListAsync(ISpecification<TEntity> spec);

        /// <summary>
        /// Adds the asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        Task<TEntity> AddAsync(TEntity entity);

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        Task UpdateAsync(TEntity entity);

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        Task DeleteAsync(TEntity entity);
        
    }

}
