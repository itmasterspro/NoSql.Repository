using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace NoSql.Repository.MongoDb.UnitTests.Utility
{
    public static class FilterUtility
    {
        public static string GetFilterExpresstion<T>(this FilterDefinition<T> filter)
        {
            var documentSerializer = BsonSerializer.SerializerRegistry.GetSerializer<T>();
            return filter.Render(documentSerializer, BsonSerializer.SerializerRegistry).ToString();
        }
    }
}
