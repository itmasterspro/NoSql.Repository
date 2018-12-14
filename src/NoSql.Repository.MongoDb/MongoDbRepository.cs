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
using MongoDB.Driver.Linq;

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
        /// <inheritdoc cref="IMongoDbRepository{TEntity}"/>
        public IMongoDbRepository<TEntity> Repository => this;

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

        #region Find & Query
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
        #endregion

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
            entities.ToList().ForEach(e=>e.Id = ObjectId.GenerateNewId());
            _collection.InsertMany(entities);
            return entities;
        }

        /// <inheritdoc />
        public IEnumerable<TEntity> Insert(IEnumerable<TEntity> entities)
        {
            var items = entities as TEntity[] ?? entities.ToArray();
            items.ToList().ForEach(e => e.Id = ObjectId.GenerateNewId());

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
            entities.ToList().ForEach(e => e.Id = ObjectId.GenerateNewId());

            await _collection.InsertManyAsync(entities).ConfigureAwait(false);
            return entities;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<TEntity>> InsertAsync(IEnumerable<TEntity> entities,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var items = entities as TEntity[] ?? entities.ToArray();
            items.ToList().ForEach(e => e.Id = ObjectId.GenerateNewId());

            await _collection.InsertManyAsync(items, null, cancellationToken).ConfigureAwait(false);
            return items;
        }

        #endregion

        #region Update methods

        /// <inheritdoc />
        public void Update(TEntity entity)
        {
            _collection.ReplaceOne(e=> e.Id == entity.Id, entity);
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
        public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken))
        {
            await _collection.ReplaceOneAsync(c => c.Id == entity.Id, entity, null, cancellationToken);
        }

        /// <inheritdoc />
        public async Task UpdateAsync(params TEntity[] entities)
        {
            var tasks = entities.Select(item=> UpdateAsync(item));
            await Task.WhenAll(tasks).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task UpdateAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default(CancellationToken))
        {
            var tasks = entities.Select(item => UpdateAsync(item, cancellationToken));
            await Task.WhenAll(tasks).ConfigureAwait(false);
        }
        #endregion

        #region Delete methods

        /// <inheritdoc />
        public void Delete(object id)
        {
            if (ObjectId.TryParse(id.ToString(), out var objectId))
            {
                _collection.DeleteOne(e=>e.Id == objectId);
            }
        }

        /// <inheritdoc />
        public void Delete(TEntity entity)
        {
            _collection.DeleteOne(e => e.Id == entity.Id);
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
        public async Task DeleteAsync(object id, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (ObjectId.TryParse(id.ToString(), out var objectId))
            {
                await _collection.DeleteOneAsync(e => e.Id == objectId, cancellationToken).ConfigureAwait(false);
            }
        }

        /// <inheritdoc />
        public async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken))
        {
            await _collection.DeleteOneAsync(e=>e.Id == entity.Id, cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task DeleteAsync(params TEntity[] entities)
        {
            var deletedIds = entities.Select(x => x.Id);
            await _collection.DeleteManyAsync(x => deletedIds.Contains(x.Id)).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task DeleteAsync(IEnumerable<TEntity> entities,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var tasks = entities.Select(item => DeleteAsync(item, cancellationToken));
            await Task.WhenAll(tasks).ConfigureAwait(false);
        }
        #endregion
    }
}
