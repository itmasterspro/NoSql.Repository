using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ItMastersPro.NoSql.Repository.Interfaces
{
    /// <summary>
    /// Defines the interfaces for synchronous CRUD operations with NoSQL collection
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public partial interface IOperation<TEntity> where TEntity : class, IEntity
    {
        /// <summary>
        /// Filters a sequence of values based on a predicate. This method default no-tracking query.
        /// </summary>
        /// <param name="predicate">An optional function to test each element for a condition.</param>
        /// <returns>An <see cref="IQueryable"/> that contains elements that satisfy the condition specified by <paramref name="predicate"/>.</returns>
        /// <remarks>This method default no-tracking query.</remarks>
        IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> predicate = null);

        /// <summary>
        /// Finds an entity with the given primary key values. If found, is attached to the context and returned. If no entity is found, then null is returned.
        /// </summary>
        /// <param name="predicate">The values of the primary key for the entity to be found.</param>
        /// <returns>The found entity or null.</returns>
        TEntity Find(Expression<Func<TEntity, bool>> predicate = null);

        /// <summary>
        /// Inserts a new entity synchronously.
        /// </summary>
        /// <param name="entity">The result contains inserted entity.</param>
        TEntity Insert(TEntity entity);

        /// <summary>
        /// Inserts a range of entities synchronously.
        /// </summary>
        /// <param name="entities">The entities to insert.</param>
        /// <returns>The result contains inserted entities.</returns>
        TEntity[] Insert(params TEntity[] entities);

        /// <summary>
        /// Inserts a range of entities synchronously.
        /// </summary>
        /// <param name="entities">The entities to insert.</param>
        /// <returns>The result contains inserted entities.</returns>
        IEnumerable<TEntity> Insert(IEnumerable<TEntity> entities);

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Update(TEntity entity);

        /// <summary>
        /// Updates the specified entities.
        /// </summary>
        /// <param name="entities">The entities.</param>
        void Update(params TEntity[] entities);

        /// <summary>
        /// Updates the specified entities.
        /// </summary>
        /// <param name="entities">The entities.</param>
        void Update(IEnumerable<TEntity> entities);

        /// <summary>
        /// Deletes the entity by the specified primary key.
        /// </summary>
        /// <param name="id">The primary key value.</param>
        void Delete(object id);

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        void Delete(TEntity entity);

        /// <summary>
        /// Deletes the specified entities.
        /// </summary>
        /// <param name="entities">The entities.</param>
        void Delete(params TEntity[] entities);

        /// <summary>
        /// Deletes the specified entities.
        /// </summary>
        /// <param name="entities">The entities.</param>
        void Delete(IEnumerable<TEntity> entities);
    }
}
