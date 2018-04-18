using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ItMastersPro.NoSql.Repository.MongoDb.Extensions
{
    /// <summary>
    /// Extensions for working with MongoDb filters
    /// </summary>
    public static class FiltersExtensions
    {
        /// <summary>
        /// Get a filter to retrieve an entity by ID
        /// <see cref="FilterDefinition{TDocument}"/>
        /// </summary>
        /// <typeparam name="TEntity">The type of the document</typeparam>
        /// <param name="id">Id entity</param>
        /// <returns>Filter for id of entity</returns>
        public static FilterDefinition<TEntity> GetByIdFilter<TEntity>(this ObjectId id)
        {
            return Builders<TEntity>.Filter.Eq("Id", id);
        }
    }
}
