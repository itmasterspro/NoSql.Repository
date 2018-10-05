using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ItMastersPro.NoSql.Repository.Interfaces;
using ItMastersPro.NoSql.Repository.MongoDb.Interfaces;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using MongoDB.Bson;
using MongoDB.Driver;
using Xunit;

namespace NoSql.Repository.MongoDb.IntegrationTests
{
    public class TesTEntityForInsertTest : IEntity
    {
        public string StringsTestField { get; set; }

        public int IntTestField { get; set; }
        
        public ObjectId Id { get; set; }
    }

    public class InsertInRepositoryIntegrationTests
    {
        private readonly IServiceProvider _testServiceProvider;

        public InsertInRepositoryIntegrationTests()
        {
            // Arrange
            _testServiceProvider = WebHost.CreateDefaultBuilder().UseStartup<Startup>().Build().Services;
        }

        [Fact]
        public void Insert_One_Entity()
        {
            // Arrange
            var mongoDbContextService = (IMongoDbContext)_testServiceProvider.GetService(typeof(IMongoDbContext));
            var serviceRepository1 = (IRepository<TesTEntityForInsertTest>)_testServiceProvider.GetService(typeof(IRepository<TesTEntityForInsertTest>));
            DropCollection(mongoDbContextService, serviceRepository1.CollectionName);

            var tesTEntity = new TesTEntityForInsertTest { IntTestField = 1, StringsTestField = "String 1", Id = new ObjectId() };

            // Act
            serviceRepository1.Insert(tesTEntity);

            var filter = Builders<TesTEntityForInsertTest>.Filter.Eq("Id", tesTEntity.Id);
            var entityInBd = mongoDbContextService.DbContext.GetCollection<TesTEntityForInsertTest>(serviceRepository1.CollectionName).Find(filter).FirstOrDefault();

            // Assert
            Assert.NotNull(entityInBd);
            Assert.Equal(entityInBd.Id, tesTEntity.Id);

            // Drop collection
            DropCollection(mongoDbContextService, serviceRepository1.CollectionName);
        }

        [Fact]
        public void Insert_Array_Entity()
        {
            // Arrange
            var mongoDbContextService = (IMongoDbContext)_testServiceProvider.GetService(typeof(IMongoDbContext));
            var serviceRepository1 = (IRepository<TesTEntityForInsertTest>)_testServiceProvider.GetService(typeof(IRepository<TesTEntityForInsertTest>));
            DropCollection(mongoDbContextService, serviceRepository1.CollectionName);

            var tesTEntityArray = new[]
            {
                new TesTEntityForInsertTest { IntTestField = 1, StringsTestField = "String 1", Id = new ObjectId() },
                new TesTEntityForInsertTest { IntTestField = 2, StringsTestField = "String 2", Id = new ObjectId() },
                new TesTEntityForInsertTest { IntTestField = 3, StringsTestField = "String 3", Id = new ObjectId() }
            };

            // Act
            serviceRepository1.Insert(tesTEntityArray);

            var filter1 = Builders<TesTEntityForInsertTest>.Filter.Eq("Id", tesTEntityArray[0].Id);
            var entity1 = mongoDbContextService.DbContext.GetCollection<TesTEntityForInsertTest>(serviceRepository1.CollectionName).Find(filter1).FirstOrDefault();
            var filter2 = Builders<TesTEntityForInsertTest>.Filter.Eq("Id", tesTEntityArray[1].Id);
            var entity2 = mongoDbContextService.DbContext.GetCollection<TesTEntityForInsertTest>(serviceRepository1.CollectionName).Find(filter2).FirstOrDefault();
            var filter3 = Builders<TesTEntityForInsertTest>.Filter.Eq("Id", tesTEntityArray[2].Id);
            var entity3 = mongoDbContextService.DbContext.GetCollection<TesTEntityForInsertTest>(serviceRepository1.CollectionName).Find(filter3).FirstOrDefault();


            // Assert
            Assert.NotNull(entity1);
            Assert.Equal(entity1.Id, tesTEntityArray[0].Id);

            Assert.NotNull(entity2);
            Assert.Equal(entity2.Id, tesTEntityArray[1].Id);

            Assert.NotNull(entity3);
            Assert.Equal(entity3.Id, tesTEntityArray[2].Id);

            // Drop collection
            DropCollection(mongoDbContextService, serviceRepository1.CollectionName);
        }

        [Fact]
        public void Insert_IEnumerable_Entity()
        {
            // Arrange
            var mongoDbContextService = (IMongoDbContext)_testServiceProvider.GetService(typeof(IMongoDbContext));
            var repository1 = (IRepository<TesTEntityForInsertTest>)_testServiceProvider.GetService(typeof(IRepository<TesTEntityForInsertTest>));
            var serviceRepository1 = repository1;
            DropCollection(mongoDbContextService, serviceRepository1.CollectionName);

            var tesTEntityForInsertTestArray = new List<TesTEntityForInsertTest>
            {
                new TesTEntityForInsertTest { IntTestField = 1, StringsTestField = "String 1", Id = new ObjectId() },
                new TesTEntityForInsertTest { IntTestField = 2, StringsTestField = "String 2", Id = new ObjectId() },
                new TesTEntityForInsertTest { IntTestField = 3, StringsTestField = "String 3", Id = new ObjectId() }
            };

            // Act
            serviceRepository1.Insert(tesTEntityForInsertTestArray);

            var filter1 = Builders<TesTEntityForInsertTest>.Filter.Eq("Id", tesTEntityForInsertTestArray[0].Id);
            var entity1 = mongoDbContextService.DbContext.GetCollection<TesTEntityForInsertTest>(serviceRepository1.CollectionName).Find(filter1).FirstOrDefault();
            var filter2 = Builders<TesTEntityForInsertTest>.Filter.Eq("Id", tesTEntityForInsertTestArray[1].Id);
            var entity2 = mongoDbContextService.DbContext.GetCollection<TesTEntityForInsertTest>(serviceRepository1.CollectionName).Find(filter2).FirstOrDefault();
            var filter3 = Builders<TesTEntityForInsertTest>.Filter.Eq("Id", tesTEntityForInsertTestArray[2].Id);
            var entity3 = mongoDbContextService.DbContext.GetCollection<TesTEntityForInsertTest>(serviceRepository1.CollectionName).Find(filter3).FirstOrDefault();


            // Assert
            Assert.NotNull(entity1);
            Assert.Equal(entity1.Id, tesTEntityForInsertTestArray[0].Id);

            Assert.NotNull(entity2);
            Assert.Equal(entity2.Id, tesTEntityForInsertTestArray[1].Id);

            Assert.NotNull(entity3);
            Assert.Equal(entity3.Id, tesTEntityForInsertTestArray[2].Id);

            // Drop collection
            DropCollection(mongoDbContextService, serviceRepository1.CollectionName);
        }

        [Fact]
        public async Task InsertAsync_One_Entity()
        {
            // Arrange
            var mongoDbContextService = (IMongoDbContext)_testServiceProvider.GetService(typeof(IMongoDbContext));
            var serviceRepository1 = (IRepository<TesTEntityForInsertTest>)_testServiceProvider.GetService(typeof(IRepository<TesTEntityForInsertTest>));
            await DropCollectionAsync(mongoDbContextService, serviceRepository1.CollectionName);

            var tesTEntityForInsertTest = new TesTEntityForInsertTest { IntTestField = 1, StringsTestField = "String 1", Id = new ObjectId() };

            // Act
            await Task.Run(async () => await serviceRepository1.InsertAsync(tesTEntityForInsertTest));

            var filter = Builders<TesTEntityForInsertTest>.Filter.Eq("Id", tesTEntityForInsertTest.Id);
            var entityInBd = mongoDbContextService.DbContext.GetCollection<TesTEntityForInsertTest>(serviceRepository1.CollectionName).Find(filter).FirstOrDefault();

            // Assert
            Assert.NotNull(entityInBd);
            Assert.Equal(entityInBd.Id, tesTEntityForInsertTest.Id);

            // Drop collection
            await DropCollectionAsync(mongoDbContextService, serviceRepository1.CollectionName);
        }

        [Fact]
        public async Task InsertAsync_Array_Entity()
        {
            // Arrange
            var mongoDbContextService = (IMongoDbContext)_testServiceProvider.GetService(typeof(IMongoDbContext));
            var serviceRepository1 = (IRepository<TesTEntityForInsertTest>)_testServiceProvider.GetService(typeof(IRepository<TesTEntityForInsertTest>));
            await DropCollectionAsync(mongoDbContextService, serviceRepository1.CollectionName);

            var tesTEntityForInsertTestArray = new[]
            {
                new TesTEntityForInsertTest { IntTestField = 1, StringsTestField = "String 1", Id = new ObjectId() },
                new TesTEntityForInsertTest { IntTestField = 2, StringsTestField = "String 2", Id = new ObjectId() },
                new TesTEntityForInsertTest { IntTestField = 3, StringsTestField = "String 3", Id = new ObjectId() }
            };

            // Act
            await Task.Run(async () => await serviceRepository1.InsertAsync(tesTEntityForInsertTestArray));

            var filter1 = Builders<TesTEntityForInsertTest>.Filter.Eq("Id", tesTEntityForInsertTestArray[0].Id);
            var entity1 = mongoDbContextService.DbContext.GetCollection<TesTEntityForInsertTest>(serviceRepository1.CollectionName).Find(filter1).FirstOrDefault();
            var filter2 = Builders<TesTEntityForInsertTest>.Filter.Eq("Id", tesTEntityForInsertTestArray[1].Id);
            var entity2 = mongoDbContextService.DbContext.GetCollection<TesTEntityForInsertTest>(serviceRepository1.CollectionName).Find(filter2).FirstOrDefault();
            var filter3 = Builders<TesTEntityForInsertTest>.Filter.Eq("Id", tesTEntityForInsertTestArray[2].Id);
            var entity3 = mongoDbContextService.DbContext.GetCollection<TesTEntityForInsertTest>(serviceRepository1.CollectionName).Find(filter3).FirstOrDefault();


            // Assert
            Assert.NotNull(entity1);
            Assert.Equal(entity1.Id, tesTEntityForInsertTestArray[0].Id);

            Assert.NotNull(entity2);
            Assert.Equal(entity2.Id, tesTEntityForInsertTestArray[1].Id);

            Assert.NotNull(entity3);
            Assert.Equal(entity3.Id, tesTEntityForInsertTestArray[2].Id);

            // Drop collection
            await DropCollectionAsync(mongoDbContextService, serviceRepository1.CollectionName);
        }

        [Fact]
        public async Task InsertAsync_IEnumerable_Entity()
        {
            // Arrange
            var mongoDbContextService = (IMongoDbContext)_testServiceProvider.GetService(typeof(IMongoDbContext));
            var serviceRepository1 = (IRepository<TesTEntityForInsertTest>)_testServiceProvider.GetService(typeof(IRepository<TesTEntityForInsertTest>));
            await DropCollectionAsync(mongoDbContextService, serviceRepository1.CollectionName);

            var tesTEntityForInsertTestArray = new List<TesTEntityForInsertTest>
            {
                new TesTEntityForInsertTest { IntTestField = 1, StringsTestField = "String 1", Id = new ObjectId() },
                new TesTEntityForInsertTest { IntTestField = 2, StringsTestField = "String 2", Id = new ObjectId() },
                new TesTEntityForInsertTest { IntTestField = 3, StringsTestField = "String 3", Id = new ObjectId() }
            };

            // Act
            await Task.Run(async () => await serviceRepository1.InsertAsync(tesTEntityForInsertTestArray));

            var filter1 = Builders<TesTEntityForInsertTest>.Filter.Eq("Id", tesTEntityForInsertTestArray[0].Id);
            var entity1 = mongoDbContextService.DbContext.GetCollection<TesTEntityForInsertTest>(serviceRepository1.CollectionName).Find(filter1).FirstOrDefault();
            var filter2 = Builders<TesTEntityForInsertTest>.Filter.Eq("Id", tesTEntityForInsertTestArray[1].Id);
            var entity2 = mongoDbContextService.DbContext.GetCollection<TesTEntityForInsertTest>(serviceRepository1.CollectionName).Find(filter2).FirstOrDefault();
            var filter3 = Builders<TesTEntityForInsertTest>.Filter.Eq("Id", tesTEntityForInsertTestArray[2].Id);
            var entity3 = mongoDbContextService.DbContext.GetCollection<TesTEntityForInsertTest>(serviceRepository1.CollectionName).Find(filter3).FirstOrDefault();


            // Assert
            Assert.NotNull(entity1);
            Assert.Equal(entity1.Id, tesTEntityForInsertTestArray[0].Id);

            Assert.NotNull(entity2);
            Assert.Equal(entity2.Id, tesTEntityForInsertTestArray[1].Id);

            Assert.NotNull(entity3);
            Assert.Equal(entity3.Id, tesTEntityForInsertTestArray[2].Id);

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
    }
}
