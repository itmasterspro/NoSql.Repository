using System;
using System.Collections.Generic;
using System.Text;
using ItMastersPro.NoSql.Repository.MongoDb;
using Xunit;

namespace NoSql.Repository.MongoDb.UnitTests
{
    public class MongoDbContextUnitTests
    {
        [Fact]
        public void Throw_Exception_If_ConnectionString_Empty()
        {
            // Assert
            Assert.Throws<ArgumentException>(() => new MongoDbContext(string.Empty));
        }

        [Fact]
        public void Throw_Exception_If_DataBaseName_Empty()
        {
            // Assert
            Assert.Throws<ArgumentException>(() => new MongoDbContext("mongodb://username:password@localhost:1234/"));
        }
    }
}
