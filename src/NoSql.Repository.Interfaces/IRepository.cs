using System.Linq;

namespace ItMastersPro.NoSql.Repository.Interfaces
{
    /// <summary>
    /// Defines the interfaces for generic repository.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public interface IRepository<TEntity> : IOperation<TEntity>, IOperationsAsync<TEntity>
        where TEntity : class, IEntity
    {
        /// <summary>
        /// Collection name in NoSQL store
        /// </summary>
        string CollectionName { get; }

        /// <summary>
        /// A DbSet represents the collection of all entities in the context, or that can be queried from the database, of a given type. 
        /// Collection objects are created from a NoSQL store using the native collection method.
        /// </summary>
        IQueryable<TEntity> Collection { get; }
    }
}
