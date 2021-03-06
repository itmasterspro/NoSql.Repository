﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace ItMastersPro.NoSql.Repository.Interfaces
{
    /// <summary>
    /// Defines the interfaces for asynchronous CRUD operations with NoSQL collection.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public interface IOperationsAsync<TEntity> where TEntity : class, IEntity
    {
        /// <summary>
        /// Finds an entity with the given primary key values. If found, is attached to the context and returned. If no entity is found, then null is returned.
        /// </summary>
        /// <param name="predicate">The values of the primary key for the entity to be found.</param>
        /// <returns>A <see cref="Task{TResult}"/> that represents the asynchronous find operation. The task result contains the found entity or null.</returns>
        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate = null);

        /// <summary>
        /// Finds an entity with the given primary key values. If found, is attached to the context and returned. If no entity is found, then null is returned.
        /// </summary>
        /// <param name="predicate">The values of the primary key for the entity to be found.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A <see cref="Task{TEntity}"/> that represents the asynchronous find operation. The task result contains the found entity or null.</returns>
        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);

        /// <summary>
        /// Inserts a new entity asynchronously.
        /// </summary>
        /// <param name="entity">The entity to insert.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A <see cref="Task{TEntity}"/> that represents the asynchronous insert operation. The task result contains inserted entity.</returns>
        Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Inserts a range of entities asynchronously.
        /// </summary>
        /// <param name="entities">The entities to insert.</param>
        /// <returns>A <see cref="Task{TResult}"/> that represents the asynchronous insert operation. The task result contains inserted entities.</returns>
        Task<TEntity[]> InsertAsync(params TEntity[] entities);

        /// <summary>
        /// Inserts a range of entities asynchronously.
        /// </summary>
        /// <param name="entities">The entities to insert.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A <see cref="Task{TResult}"/> that represents the asynchronous insert operation. The task result contains inserted entities</returns>
        Task<IEnumerable<TEntity>> InsertAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Updates the specified entity asynchronously.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Updates the specified entities asynchronously.
        /// </summary>
        /// <param name="entities">The entities.</param>
        Task UpdateAsync(params TEntity[] entities);

        /// <summary>
        /// Updates the specified entities asynchronously.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        Task UpdateAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Deletes the entity by the specified primary key asynchronously.
        /// </summary>
        /// <param name="id">The primary key value.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        Task DeleteAsync(object id, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Deletes the specified entity asynchronously.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Deletes the specified entities asynchronously.
        /// </summary>
        /// <param name="entities">The entities.</param>
        Task DeleteAsync(params TEntity[] entities);

        /// <summary>
        /// Deletes the specified entities asynchronously.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        Task DeleteAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default(CancellationToken));
    }
}
