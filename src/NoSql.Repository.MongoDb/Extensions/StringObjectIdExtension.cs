using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;

namespace ItMastersPro.NoSql.Repository.MongoDb.Extensions
{
    /// <summary>
    /// The extension for convert string to ObjectId
    /// </summary>
    public static class StringObjectIdExtension
    {
        /// <summary>
        /// Convert string to ObjectId
        /// </summary>
        /// <param name="strObjectId">Source string</param>
        /// <returns>Converted ObjectId</returns>
        public static ObjectId ToObjectId(this string strObjectId)
        {
            if (string.IsNullOrWhiteSpace(strObjectId))
            {
                return ObjectId.Empty;
            }

            if (!ObjectId.TryParse(strObjectId, out ObjectId objId))
            {
                throw new ArgumentException($"'{strObjectId}' is not an ObjectId.");
            }
            return objId;
        }

        /// <summary>
        /// Convert strings collection to ObjectId collection
        /// </summary>
        /// <param name="strObjectIds">Strings collection</param>
        /// <returns>ObjectId collection</returns>
        public static ICollection<ObjectId> ToObjectIdCollection(this ICollection<string> strObjectIds)
        {
            return strObjectIds.Select(ToObjectId).ToList();
        }

        /// <summary>
        /// Convert string to <see cref="Nullable{ObjectId}"/>
        /// </summary>
        /// <param name="strObjectId">Source string</param>
        /// <returns>Converted <see cref="Nullable{ObjectId}"/></returns>
        public static ObjectId? ToNullableObjectId(this string strObjectId)
        {
            if (string.IsNullOrWhiteSpace(strObjectId)) return null;

            if (!ObjectId.TryParse(strObjectId, out var objectId))
                throw new ArgumentException($"'{strObjectId}' is not an ObjectId.");

            return objectId;
        }
    }
}
