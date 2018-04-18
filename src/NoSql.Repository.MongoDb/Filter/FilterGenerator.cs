using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using MongoDB.Driver;

namespace ItMastersPro.NoSql.Repository.MongoDb.Filter
{
    /// <summary>
    /// Filter generator for entity based on another class
    /// </summary>
    /// <typeparam name="TEntity">The class for which the filter is generated</typeparam>
    /// <typeparam name="TSource">The class from which the filter is generated</typeparam>
    public abstract class FilterGenerator<TEntity, TSource>
    {
        protected readonly TSource _data;

        protected FilterGenerator(TSource data)
        {
            _data = data;
        }

        /// <summary>
        /// Get a list of filters to extract items from the collection
        /// </summary>
        /// <returns>List of filters <see cref="FilterDefinition{TDocument}"/></returns>
        public IList<FilterDefinition<TEntity>> GetFiltersList()
        {
            var conditions = new List<FilterDefinition<TEntity>>();
            var methods = GetType().GetMethods(BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (var method in methods)
            {
                if (method?.Invoke(this, null) is FilterDefinition<TEntity> filter)
                {
                    conditions.Add(filter);
                }
            }

            return conditions;
        }

        /// <summary>
        /// Create AND filter as <see cref="FilterDefinition{TEntity}"/>
        /// </summary>
        /// <returns>AND filter based on data from another class</returns>
        public FilterDefinition<TEntity> And()
        {
            var conditions = GetFiltersList();
            var result = Builders<TEntity>.Filter.And(conditions);

            return result;
        }

        /// <summary>
        /// Create OR filter as <see cref="FilterDefinition{TEntity}"/>
        /// </summary>
        /// <returns>OR filter based on data from another class</returns>
        public FilterDefinition<TEntity> Or()
        {
            var conditions = GetFiltersList();
            var result = Builders<TEntity>.Filter.Or(conditions);

            return result;
        }
    }
}
