using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace ItMastersPro.NoSql.Repository.Interfaces
{
    /// <summary>
    /// Defines the interfaces for MongoDb generic repository.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>

    public interface IMongoDbRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        /// <summary>
        /// MongodDb generic repository
        /// </summary>
        IMongoDbRepository<TEntity> Repository { get; }

        /// <summary>
        /// Filters a sequence of values based on a filter definition. This method default no-tracking query.
        /// </summary>
        /// <param name="filter">Filter to test each element for a condition.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/> that contains elements that satisfy the condition specified by <paramref name="filter"/>.</returns>
        IQueryable<TEntity> Query(FilterDefinition<TEntity> filter);

        /// <summary>
        /// Filters a sequence of values based on a filter definition. This method default no-tracking query.
        /// </summary>
        /// <param name="filter">Filter to test each element for a condition.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/> that contains elements that satisfy the condition specified by <paramref name="filter"/>.</returns>
        Task<IQueryable<TEntity>> QueryAsync(FilterDefinition<TEntity> filter);
    }
}
