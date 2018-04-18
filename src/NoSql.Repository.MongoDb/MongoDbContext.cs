using System;
using System.Collections.Generic;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using ItMastersPro.NoSql.Repository.Interfaces;
using ItMastersPro.NoSql.Repository.MongoDb.Extensions;
using ItMastersPro.NoSql.Repository.MongoDb.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ItMastersPro.NoSql.Repository.MongoDb
{
    /// <summary>
    /// Class for combination of the Unit Of Work and Repository patterns such that 
    /// it can be used to query from a Mongo database and group together changes that 
    /// will then be written back to the store as a unit.
    /// </summary>
    public class MongoDbContext : IMongoDbContext
    {

        #region Fields
        private Dictionary<string, object> _collectionsMongoDb;
        #endregion

        #region Properties
        /// <inheritdoc />
        public MongoClientSettings Settings { get; }

        /// <inheritdoc />
        public string DatabaseName { get; }

        /// <inheritdoc />
        public IMongoDatabase DbContext { get; }
        #endregion

        public MongoDbContext(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentException("Connection string can't be null or empty.");

            try
            {
                var mongoUrl = new MongoUrl(connectionString);
                Settings = MongoClientSettings.FromUrl(mongoUrl);

                if (string.IsNullOrWhiteSpace(mongoUrl.DatabaseName))
                    throw new ArgumentException("Database name can't be null or empty.");

                DatabaseName = mongoUrl.DatabaseName;

                if (Settings.UseSsl)
                {
                    Settings.SslSettings = new SslSettings
                    {
                        EnabledSslProtocols = SslProtocols.Tls12
                    };
                }

                var mongoClient = new MongoClient(Settings);
                DbContext = mongoClient.GetDatabase(DatabaseName);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <inheritdoc />
        public async Task<IMongoCollection<TEntity>> GetCollectionAsync<TEntity>() where TEntity : class, IEntity
        {
            if (_collectionsMongoDb == null)
            {
                _collectionsMongoDb = new Dictionary<string, object>();
            }
            var collectionName = typeof(TEntity).MongoCollectionName();

            if (false == await IsCollectionExistsAsync<TEntity>().ConfigureAwait(false))
            {
                await DbContext.CreateCollectionAsync(collectionName).ConfigureAwait(false);
            }

            if (!_collectionsMongoDb.ContainsKey(collectionName))
            {
                _collectionsMongoDb[collectionName] = DbContext.GetCollection<TEntity>(collectionName);
            }

            return (IMongoCollection<TEntity>)_collectionsMongoDb[collectionName];
        }

        /// <inheritdoc />
        public async Task<bool> IsCollectionExistsAsync<TEntity>()
        {
            var filter = new BsonDocument("name", typeof(TEntity).MongoCollectionName());
            //filter by collection name
            var collections = await DbContext.ListCollectionsAsync(new ListCollectionsOptions { Filter = filter }).ConfigureAwait(false);
            //check for existence
            return await collections.AnyAsync().ConfigureAwait(false);
        }
    }
}
