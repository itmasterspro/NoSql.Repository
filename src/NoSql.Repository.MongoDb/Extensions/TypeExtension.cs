using System;
using System.Collections.Generic;
using System.Text;

namespace ItMastersPro.NoSql.Repository.MongoDb.Extensions
{
    /// <summary>
    /// Extensions for get collection name by type of entity
    /// </summary>
    internal static class TypeExtension
    {
        /// <summary>
        /// Get collection name by name of document type
        /// </summary>
        /// <param name="type">The type of the document</param>
        /// <returns>Name collection in MongoDb</returns>
        internal static string MongoCollectionName(this Type type)
        {
            var collectionName = type.Name.GetPluralizationName().ToLowerInvariant();
            if (string.IsNullOrWhiteSpace(collectionName))
                throw new ArgumentNullException($"Collection name can not be empty!");
            return collectionName;
        }
    }
}
