using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ItMastersPro.NoSql.Repository.Interfaces;
using ItMastersPro.NoSql.Repository.MongoDb.Extensions;
using ItMastersPro.NoSql.Repository.MongoDb.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ItMastersPro.NoSql.Repository.MongoDb
{
    /// <summary>
    /// Represents a default generic repository that implements an interface <see cref = "IRepository{TEntity}" />.
    /// </summary>
    /// <typeparam name="TEntity">Тип сущности.</typeparam>
    public class MongoDbRepository<TEntity> : IMongoDbRepository<TEntity> where TEntity : class, IEntity
    {
        #region Fields

        private readonly IMongoCollection<TEntity> _collection;

        #endregion

        #region Properties

        /// <inheritdoc cref="IRepository{TEntity}"/>
        public string CollectionName => typeof(TEntity).MongoCollectionName();

        /// <inheritdoc cref="IRepository{TEntity}"/>
        public IQueryable<TEntity> Collection { get; }
        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public MongoDbRepository(IMongoDbContext dbContext)
        {
            dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _collection = dbContext.DbContext.GetCollection<TEntity>(CollectionName);
            Collection = _collection.AsQueryable();
        }

        #endregion

        /// <inheritdoc />
        public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> predicate = null)
        {
            return predicate != null ? Collection.Where(predicate) : Collection;
        }

        /// <inheritdoc />
        public IQueryable<TEntity> Query(FilterDefinition<TEntity> filter)
        {
            return filter != null ? _collection.Find(filter).ToList().AsQueryable() : Collection;
        }

        /// <inheritdoc />
        public async Task<IQueryable<TEntity>> QueryAsync(FilterDefinition<TEntity> filter)
        {
            return filter != null
                ? (await _collection.FindAsync(filter).ConfigureAwait(false)).ToList().AsQueryable()
                : Collection;
        }

        /// <inheritdoc />
        public TEntity Find(Expression<Func<TEntity, bool>> predicate = null)
        {
            return predicate != null ? Collection.SingleOrDefault(predicate) : Collection.SingleOrDefault();
        }

        /// <inheritdoc />
        public async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate = null) =>
            (await _collection.FindAsync(predicate).ConfigureAwait(false)).SingleOrDefault();

        /// <inheritdoc />
        public async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken)
            => (await _collection.FindAsync(predicate, null, cancellationToken).ConfigureAwait(false))
                .SingleOrDefault();

        #region Insert methods

        /// <inheritdoc />
        public TEntity Insert(TEntity entity)
        {
            entity.Id = ObjectId.GenerateNewId();
            _collection.InsertOne(entity);
            return entity;
        }

        /// <inheritdoc />
        public TEntity[] Insert(params TEntity[] entities)
        {
            foreach (var item in entities)
            {
                item.Id = ObjectId.GenerateNewId();
            }

            _collection.InsertMany(entities);
            return entities;
        }

        /// <inheritdoc />
        public IEnumerable<TEntity> Insert(IEnumerable<TEntity> entities)
        {
            var items = entities.ToArray();
            foreach (var item in items)
            {
                item.Id = ObjectId.GenerateNewId();
            }

            _collection.InsertMany(items);
            return items;
        }

        /// <inheritdoc />
        public async Task<TEntity> InsertAsync(TEntity entity,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            entity.Id = ObjectId.GenerateNewId();
            await _collection.InsertOneAsync(entity, null, cancellationToken).ConfigureAwait(false);
            return entity;
        }

        /// <inheritdoc />
        public async Task<TEntity[]> InsertAsync(params TEntity[] entities)
        {
            foreach (var item in entities)
            {
                item.Id = ObjectId.GenerateNewId();
            }

            await _collection.InsertManyAsync(entities).ConfigureAwait(false);
            return entities;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<TEntity>> InsertAsync(IEnumerable<TEntity> entities,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var items = entities.ToArray();
            foreach (var item in items)
            {
                item.Id = ObjectId.GenerateNewId();
            }

            await _collection.InsertManyAsync(items, null, cancellationToken).ConfigureAwait(false);
            return items;
        }

        #endregion

        #region Update methods

        /// <inheritdoc />
        public void Update(TEntity entity)
        {
            if (ObjectId.TryParse(entity.Id.ToString(), out var objectId))
            {
                _collection.ReplaceOne(objectId.GetByIdFilter<TEntity>(), entity);
            }
        }

        /// <inheritdoc />
        public void Update(params TEntity[] entities)
        {
            foreach (var itemEntity in entities)
            {
                Update(itemEntity);
            }
        }

        /// <inheritdoc />
        public void Update(IEnumerable<TEntity> entities)
        {
            Update(entities.ToArray());
        }

        /// <inheritdoc />
        public async Task UpdateAsync(TEntity entity)
        {
            if (ObjectId.TryParse(entity.Id.ToString(), out var objectId))
            {
                await _collection.ReplaceOneAsync(objectId.GetByIdFilter<TEntity>(), entity).ConfigureAwait(false);
            }
        }

        /// <inheritdoc />
        public async Task UpdateAsync(params TEntity[] entities)
        {
            foreach (var itemEntity in entities)
            {
                await UpdateAsync(itemEntity).ConfigureAwait(false);
            }
        }

        /// <inheritdoc />
        public async Task UpdateAsync(IEnumerable<TEntity> entities)
        {
            await UpdateAsync(entities.ToArray()).ConfigureAwait(false);
        }

        #endregion

        #region Delete methods

        /// <inheritdoc />
        public void Delete(object id)
        {
            if (ObjectId.TryParse(id.ToString(), out var objectId))
            {
                _collection.DeleteOne(objectId.GetByIdFilter<TEntity>());
            }
        }

        /// <inheritdoc />
        public void Delete(TEntity entity)
        {
            if (ObjectId.TryParse(entity.Id.ToString(), out var objectId))
            {
                _collection.DeleteOne(objectId.GetByIdFilter<TEntity>());
            }
        }

        /// <inheritdoc />
        public void Delete(params TEntity[] entities)
        {
            var deletedIds = entities.Select(x => x.Id);
            _collection.DeleteMany(x => deletedIds.Contains(x.Id));
        }

        /// <inheritdoc />
        public void Delete(IEnumerable<TEntity> entities)
        {
            Delete(entities.ToArray());
        }

        /// <inheritdoc />
        public async Task DeleteAsync(object id)
        {
            if (ObjectId.TryParse(id.ToString(), out var objectId))
            {
                await _collection.DeleteOneAsync(objectId.GetByIdFilter<TEntity>()).ConfigureAwait(false);
            }
        }

        /// <inheritdoc />
        public async Task DeleteAsync(TEntity entity)
        {
            if (ObjectId.TryParse(entity.Id.ToString(), out var objectId))
            {
                await _collection.DeleteOneAsync(objectId.GetByIdFilter<TEntity>()).ConfigureAwait(false);
            }
        }

    /// <inheritdoc />
        public async Task DeleteAsync(params TEntity[] entities)
        {
            var deletedIds = entities.Select(x => x.Id);
            await _collection.DeleteManyAsync(x => deletedIds.Contains(x.Id)).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task DeleteAsync(IEnumerable<TEntity> entities) => await DeleteAsync(entities.ToArray()).ConfigureAwait(false);
        #endregion
    }
}
