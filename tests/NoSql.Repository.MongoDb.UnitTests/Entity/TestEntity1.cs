using System;
using System.Collections.Generic;
using System.Text;
using ItMastersPro.NoSql.Repository.Interfaces;
using MongoDB.Bson;

namespace NoSql.Repository.MongoDb.UnitTests.Entity
{
    public class TesTEntity1 : IEntity
    {
        public string StringsTestField { get; set; }

        public int IntTestField { get; set; }
        public ObjectId Id { get; set; }
    }
}
