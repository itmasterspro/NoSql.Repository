using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ItMastersPro.NoSql.Repository.Interfaces;
using ItMastersPro.NoSql.Repository.MongoDb.Interfaces;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using MongoDB.Bson;
using Xunit;

namespace NoSql.Repository.MongoDb.IntegrationTests
{
    public class TesTEntityForFindTest : IEntity
    {
        public ObjectId Id { get; set; }

        public string StringsTestField { get; set; }

        public int IntTestField { get; set; }
    }


    public class FindInRepositoryIntegrationTests
    {
        private readonly IServiceProvider _testServiceProvider;

        public FindInRepositoryIntegrationTests()
        {
            // Arrange
            _testServiceProvider = WebHost.CreateDefaultBuilder().UseStartup<Startup>().Build().Services;
        }


        [Fact]
        public void Query_WithPredicate()
        {
            // Arrange
            var mongoDbContextService = (IMongoDbContext)_testServiceProvider.GetService(typeof(IMongoDbContext));
            var serviceRepository1 = (IRepository<TesTEntityForFindTest>)_testServiceProvider.GetService(typeof(IRepository<TesTEntityForFindTest>));
            DropCollection(mongoDbContextService, serviceRepository1.CollectionName);

            var tesTEntityArray = new[]
            {
                new TesTEntityForFindTest(){ IntTestField = 1, StringsTestField = "String 1", Id = new ObjectId() },
                new TesTEntityForFindTest(){ IntTestField = 2, StringsTestField = "String 2", Id = new ObjectId() },
                new TesTEntityForFindTest(){ IntTestField = 3, StringsTestField = "String 3", Id = new ObjectId() }
            };
            serviceRepository1.Insert(tesTEntityArray);


            // Act
            var entity1 = serviceRepository1.Query(x => x.IntTestField > 1);

            // Assert
            Assert.NotNull(entity1);
            Assert.Equal(2, entity1.ToList().Count);

            // Drop collection
            DropCollection(mongoDbContextService, serviceRepository1.CollectionName);
        }


        [Fact]
        public void Query_WithoutPredicate()
        {
            // Arrange
            var mongoDbContextService = (IMongoDbContext)_testServiceProvider.GetService(typeof(IMongoDbContext));
            var serviceRepository1 = (IRepository<TesTEntityForFindTest>)_testServiceProvider.GetService(typeof(IRepository<TesTEntityForFindTest>));
            DropCollection(mongoDbContextService, serviceRepository1.CollectionName);

            var tesTEntityArray = new[]
            {
                new TesTEntityForFindTest(){ IntTestField = 1, StringsTestField = "String 1", Id = new ObjectId() },
                new TesTEntityForFindTest(){ IntTestField = 2, StringsTestField = "String 2", Id = new ObjectId() },
                new TesTEntityForFindTest(){ IntTestField = 3, StringsTestField = "String 3", Id = new ObjectId() }
            };
            serviceRepository1.Insert(tesTEntityArray);


            // Act
            var entity1 = serviceRepository1.Query();

            // Assert
            Assert.NotNull(entity1);
            Assert.Equal(3, entity1.ToList().Count);

            // Drop collection
            DropCollection(mongoDbContextService, serviceRepository1.CollectionName);
        }

        [Fact]
        public void Find_Entity()
        {
            // Arrange
            var mongoDbContextService = (IMongoDbContext)_testServiceProvider.GetService(typeof(IMongoDbContext));
            var serviceRepository1 = (IRepository<TesTEntityForFindTest>)_testServiceProvider.GetService(typeof(IRepository<TesTEntityForFindTest>));
            DropCollection(mongoDbContextService, serviceRepository1.CollectionName);

            var tesTEntityArray = new[]
            {
                new TesTEntityForFindTest(){ IntTestField = 1, StringsTestField = "String 1", Id = new ObjectId() },
                new TesTEntityForFindTest(){ IntTestField = 2, StringsTestField = "String 2", Id = new ObjectId() },
                new TesTEntityForFindTest(){ IntTestField = 3, StringsTestField = "String 3", Id = new ObjectId() }
            };
            serviceRepository1.Insert(tesTEntityArray);


            // Act
            var entity1 = serviceRepository1.Find(x => x.Id == tesTEntityArray[1].Id);

            // Assert
            Assert.NotNull(entity1);
            Assert.Equal(entity1.Id, tesTEntityArray[1].Id);
            Assert.Equal(2, entity1.IntTestField);
            Assert.Equal("String 2", entity1.StringsTestField);

            // Drop collection
            DropCollection(mongoDbContextService, serviceRepository1.CollectionName);
        }

        [Fact]
        public async Task FindAsync_Entity()
        {
            // Arrange
            var mongoDbContextService = (IMongoDbContext)_testServiceProvider.GetService(typeof(IMongoDbContext));
            var serviceRepository1 = (IRepository<TesTEntityForFindTest>)_testServiceProvider.GetService(typeof(IRepository<TesTEntityForFindTest>));
            await DropCollectionAsync(mongoDbContextService, serviceRepository1.CollectionName);

            var tesTEntityArray = new[]
            {
                new TesTEntityForFindTest(){ IntTestField = 1, StringsTestField = "String 1", Id = new ObjectId() },
                new TesTEntityForFindTest(){ IntTestField = 2, StringsTestField = "String 2", Id = new ObjectId() },
                new TesTEntityForFindTest(){ IntTestField = 3, StringsTestField = "String 3", Id = new ObjectId() }
            };
            await Task.Run(async () => await serviceRepository1.InsertAsync(tesTEntityArray));


            // Act
            var entity1 = await serviceRepository1.FindAsync(predicate: x => x.Id == tesTEntityArray[1].Id);

            // Assert
            Assert.NotNull(entity1);
            Assert.Equal(entity1.Id, tesTEntityArray[1].Id);
            Assert.Equal(2, entity1.IntTestField);
            Assert.Equal("String 2", entity1.StringsTestField);

            // Drop collection
            await DropCollectionAsync(mongoDbContextService, serviceRepository1.CollectionName);
        }

        private void DropCollection(IMongoDbContext context, string collectionName)
        {
            context.DbContext.DropCollection(collectionName);
        }

        private async Task DropCollectionAsync(IMongoDbContext context, string collectionName)
        {
            await context.DbContext.DropCollectionAsync(collectionName);
        }

        [Fact]
        public void Query_NotExistedCollection()
        {
            // Arrange
            var mongoDbContextService = (IMongoDbContext)_testServiceProvider.GetService(typeof(IMongoDbContext));
            var serviceRepository1 = (IRepository<TesTEntityForFindTest>)_testServiceProvider.GetService(typeof(IRepository<TesTEntityForFindTest>));
            DropCollection(mongoDbContextService, serviceRepository1.CollectionName);

            // Act
            IEnumerable<TesTEntityForFindTest> entityList = serviceRepository1.Query().OrderBy(c => c.IntTestField).Skip(2).Take(5);
            var entity = entityList.FirstOrDefault();
            // Assert
            Assert.Null(entity);
        }
    }
}
