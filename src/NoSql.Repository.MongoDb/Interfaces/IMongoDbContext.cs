using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ItMastersPro.NoSql.Repository.Interfaces;
using MongoDB.Driver;

namespace ItMastersPro.NoSql.Repository.MongoDb.Interfaces
{
    /// <summary>
    /// Interface for combination of the Unit Of Work and Repository patterns such that 
    /// it can be used to query from a Mongo database and group together changes that 
    /// will then be written back to the store as a unit.
    /// </summary>
    public interface IMongoDbContext
    {
        /*
        /// <summary>
        /// Connection string for connecting to MongoDb
        /// </summary>
        string ConnectionString { get; }*/

        /// <summary>
        /// Mongodb client settings
        /// </summary>
        MongoClientSettings Settings { get; }

        /// <summary>
        /// Database name
        /// </summary>
        string DatabaseName { get; }

        /// <summary>
        /// Database Context
        /// </summary>
        IMongoDatabase DbContext { get; }

        /// <summary>
        /// Gets the specified repository for the <typeparamref name="TEntity"/>.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns>An instance of type inherited from <see cref="IMongoCollection{TEntity}"/> interface.</returns>
        Task<IMongoCollection<TEntity>> GetCollectionAsync<TEntity>() where TEntity : class, IEntity;

        /// <summary>
        /// The method checks in database the existence of a collection with a given name
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns> A mark of the collection in the database </returns>
        Task<bool> IsCollectionExistsAsync<TEntity>();
    }
}
