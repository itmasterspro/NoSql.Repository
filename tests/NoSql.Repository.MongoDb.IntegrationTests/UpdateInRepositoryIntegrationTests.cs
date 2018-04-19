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
    public class TesTEntityForUpdateTest : IEntity
    {
        public object Id { get; set; }

        public string StringsTestField { get; set; }

        public int IntTestField { get; set; }
    }

    public class UpdateInRepositoryIntegrationTests
    {
        private readonly IServiceProvider _testServiceProvider;

        public UpdateInRepositoryIntegrationTests()
        {
            // Arrange
            _testServiceProvider = WebHost.CreateDefaultBuilder().UseStartup<Startup>().Build().Services;
        }

        [Fact]
        public void Update_One_Entity()
        {
            // Arrange
            var serviceRepository1 = (IRepository<TesTEntityForUpdateTest>)_testServiceProvider.GetService(typeof(IRepository<TesTEntityForUpdateTest>));

            // Drop collection
            var mongoDbContextService = (IMongoDbContext)_testServiceProvider.GetService(typeof(IMongoDbContext));
            DropCollection(mongoDbContextService, serviceRepository1.CollectionName);

            var tesTEntity = new TesTEntityForUpdateTest { IntTestField = 1, StringsTestField = "String 1", Id = new ObjectId() };
            serviceRepository1.Insert(tesTEntity);

            // Act
            tesTEntity.IntTestField = 10;
            tesTEntity.StringsTestField = "String 10";
            serviceRepository1.Update(tesTEntity);

            var filter = Builders<TesTEntityForUpdateTest>.Filter.Eq("Id", tesTEntity.Id);
            var entityInBd = mongoDbContextService.DbContext.GetCollection<TesTEntityForUpdateTest>(serviceRepository1.CollectionName).Find(filter).FirstOrDefault();
            var countInCollection = mongoDbContextService.DbContext.GetCollection<TesTEntityForUpdateTest>(serviceRepository1.CollectionName).Count(x => true);

            // Assert
            Assert.NotNull(entityInBd);
            Assert.Equal(1, countInCollection);
            Assert.Equal(tesTEntity.Id, entityInBd.Id);
            Assert.Equal(10, entityInBd.IntTestField);
            Assert.Equal("String 10", entityInBd.StringsTestField);

            // Drop collection
            DropCollection(mongoDbContextService, serviceRepository1.CollectionName);
        }

        [Fact]
        public void Update_Array_Entity()
        {
            // Arrange
            var serviceRepository1 = (IRepository<TesTEntityForUpdateTest>)_testServiceProvider.GetService(typeof(IRepository<TesTEntityForUpdateTest>));
            // Drop collection
            var mongoDbContextService = (IMongoDbContext)_testServiceProvider.GetService(typeof(IMongoDbContext));
            DropCollection(mongoDbContextService, serviceRepository1.CollectionName);

            var tesTEntityArray = new TesTEntityForUpdateTest[]
            {
                new TesTEntityForUpdateTest(){ IntTestField = 1, StringsTestField = "String 1", Id = new ObjectId() },
                new TesTEntityForUpdateTest(){ IntTestField = 2, StringsTestField = "String 2", Id = new ObjectId() },
                new TesTEntityForUpdateTest(){ IntTestField = 3, StringsTestField = "String 3", Id = new ObjectId() }
            };
            serviceRepository1.Insert(tesTEntityArray);

            // Act
            tesTEntityArray[0].IntTestField = 10;
            tesTEntityArray[0].StringsTestField = "String 10";
            tesTEntityArray[1].IntTestField = 20;
            tesTEntityArray[1].StringsTestField = "String 20";
            tesTEntityArray[2].IntTestField = 30;
            tesTEntityArray[2].StringsTestField = "String 30";

            serviceRepository1.Update(tesTEntityArray);

            var filter1 = Builders<TesTEntityForUpdateTest>.Filter.Eq("Id", tesTEntityArray[0].Id);
            var entity1 = mongoDbContextService.DbContext.GetCollection<TesTEntityForUpdateTest>(serviceRepository1.CollectionName).Find(filter1).FirstOrDefault();
            var filter2 = Builders<TesTEntityForUpdateTest>.Filter.Eq("Id", tesTEntityArray[1].Id);
            var entity2 = mongoDbContextService.DbContext.GetCollection<TesTEntityForUpdateTest>(serviceRepository1.CollectionName).Find(filter2).FirstOrDefault();
            var filter3 = Builders<TesTEntityForUpdateTest>.Filter.Eq("Id", tesTEntityArray[2].Id);
            var entity3 = mongoDbContextService.DbContext.GetCollection<TesTEntityForUpdateTest>(serviceRepository1.CollectionName).Find(filter3).FirstOrDefault();
            var countInCollection = mongoDbContextService.DbContext.GetCollection<TesTEntityForUpdateTest>(serviceRepository1.CollectionName).Count(x => true);


            // Assert
            Assert.Equal(3, countInCollection);

            Assert.NotNull(entity1);
            Assert.Equal(entity1.Id, tesTEntityArray[0].Id);
            Assert.Equal(10, entity1.IntTestField);
            Assert.Equal("String 10", entity1.StringsTestField);

            Assert.NotNull(entity2);
            Assert.Equal(entity2.Id, tesTEntityArray[1].Id);
            Assert.Equal(20, entity2.IntTestField);
            Assert.Equal("String 20", entity2.StringsTestField);

            Assert.NotNull(entity3);
            Assert.Equal(entity3.Id, tesTEntityArray[2].Id);
            Assert.Equal(30, entity3.IntTestField);
            Assert.Equal("String 30", entity3.StringsTestField);

            // Drop collection
            DropCollection(mongoDbContextService, serviceRepository1.CollectionName);
        }

        [Fact]
        public void Update_IEnumerable_Entity()
        {
            // Arrange
            var serviceRepository1 = (IRepository<TesTEntityForUpdateTest>)_testServiceProvider.GetService(typeof(IRepository<TesTEntityForUpdateTest>));
            // Drop collection
            var mongoDbContextService = (IMongoDbContext)_testServiceProvider.GetService(typeof(IMongoDbContext));
            DropCollection(mongoDbContextService, serviceRepository1.CollectionName);

            var testLisTEntity = new List<TesTEntityForUpdateTest>
            {
                new TesTEntityForUpdateTest(){ IntTestField = 1, StringsTestField = "String 1", Id = new ObjectId() },
                new TesTEntityForUpdateTest(){ IntTestField = 2, StringsTestField = "String 2", Id = new ObjectId() },
                new TesTEntityForUpdateTest(){ IntTestField = 3, StringsTestField = "String 3", Id = new ObjectId() }
            };
            serviceRepository1.Insert(testLisTEntity);

            // Act
            testLisTEntity[0].IntTestField = 10;
            testLisTEntity[0].StringsTestField = "String 10";
            testLisTEntity[1].IntTestField = 20;
            testLisTEntity[1].StringsTestField = "String 20";
            testLisTEntity[2].IntTestField = 30;
            testLisTEntity[2].StringsTestField = "String 30";

            serviceRepository1.Update(testLisTEntity);

            var filter1 = Builders<TesTEntityForUpdateTest>.Filter.Eq("Id", testLisTEntity[0].Id);
            var entity1 = mongoDbContextService.DbContext.GetCollection<TesTEntityForUpdateTest>(serviceRepository1.CollectionName).Find(filter1).FirstOrDefault();
            var filter2 = Builders<TesTEntityForUpdateTest>.Filter.Eq("Id", testLisTEntity[1].Id);
            var entity2 = mongoDbContextService.DbContext.GetCollection<TesTEntityForUpdateTest>(serviceRepository1.CollectionName).Find(filter2).FirstOrDefault();
            var filter3 = Builders<TesTEntityForUpdateTest>.Filter.Eq("Id", testLisTEntity[2].Id);
            var entity3 = mongoDbContextService.DbContext.GetCollection<TesTEntityForUpdateTest>(serviceRepository1.CollectionName).Find(filter3).FirstOrDefault();
            var countInCollection = mongoDbContextService.DbContext.GetCollection<TesTEntityForUpdateTest>(serviceRepository1.CollectionName).Count(x => true);


            // Assert
            Assert.Equal(3, countInCollection);

            Assert.NotNull(entity1);
            Assert.Equal(entity1.Id, testLisTEntity[0].Id);
            Assert.Equal(10, entity1.IntTestField);
            Assert.Equal("String 10", entity1.StringsTestField);

            Assert.NotNull(entity2);
            Assert.Equal(entity2.Id, testLisTEntity[1].Id);
            Assert.Equal(20, entity2.IntTestField);
            Assert.Equal("String 20", entity2.StringsTestField);

            Assert.NotNull(entity3);
            Assert.Equal(entity3.Id, testLisTEntity[2].Id);
            Assert.Equal(30, entity3.IntTestField);
            Assert.Equal("String 30", entity3.StringsTestField);

            // Drop collection
            DropCollection(mongoDbContextService, serviceRepository1.CollectionName);
        }

        [Fact]
        public async Task UpdateAsync_One_Entity()
        {
            // Arrange
            var serviceRepository1 = (IRepository<TesTEntityForUpdateTest>)_testServiceProvider.GetService(typeof(IRepository<TesTEntityForUpdateTest>));
            // Drop collection
            var mongoDbContextService = (IMongoDbContext)_testServiceProvider.GetService(typeof(IMongoDbContext));
            await DropCollectionAsync(mongoDbContextService, serviceRepository1.CollectionName);

            var tesTEntity = new TesTEntityForUpdateTest { IntTestField = 1, StringsTestField = "String 1", Id = new ObjectId() };
            await Task.Run(async () => await serviceRepository1.InsertAsync(tesTEntity));

            // Act
            tesTEntity.IntTestField = 2;
            tesTEntity.StringsTestField = "String 2";
            await Task.Run(async () => await serviceRepository1.UpdateAsync(tesTEntity));

            var filter = Builders<TesTEntityForUpdateTest>.Filter.Eq("Id", tesTEntity.Id);
            var entityInBd = mongoDbContextService.DbContext.GetCollection<TesTEntityForUpdateTest>(serviceRepository1.CollectionName).Find(filter).FirstOrDefault();
            var countInCollection = await mongoDbContextService.DbContext.GetCollection<TesTEntityForUpdateTest>(serviceRepository1.CollectionName).CountAsync(x => true);

            // Assert
            Assert.NotNull(entityInBd);
            Assert.Equal(1, countInCollection);
            Assert.Equal(tesTEntity.Id, entityInBd.Id);
            Assert.Equal(2, entityInBd.IntTestField);
            Assert.Equal("String 2", entityInBd.StringsTestField);

            // Drop collection
            await DropCollectionAsync(mongoDbContextService, serviceRepository1.CollectionName);
        }

        [Fact]
        public async Task UpdateAsync_Array_Entity()
        {
            // Arrange
            var serviceRepository1 = (IRepository<TesTEntityForUpdateTest>)_testServiceProvider.GetService(typeof(IRepository<TesTEntityForUpdateTest>));
            // Drop collection
            var mongoDbContextService = (IMongoDbContext)_testServiceProvider.GetService(typeof(IMongoDbContext));
            await DropCollectionAsync(mongoDbContextService, serviceRepository1.CollectionName);

            var tesTEntityArray = new[]
            {
                new TesTEntityForUpdateTest { IntTestField = 1, StringsTestField = "String 1", Id = new ObjectId() },
                new TesTEntityForUpdateTest { IntTestField = 2, StringsTestField = "String 2", Id = new ObjectId() },
                new TesTEntityForUpdateTest { IntTestField = 3, StringsTestField = "String 3", Id = new ObjectId() }
            };
            await Task.Run(async () => await serviceRepository1.InsertAsync(tesTEntityArray));

            // Act
            tesTEntityArray[0].IntTestField = 10;
            tesTEntityArray[0].StringsTestField = "String 10";
            tesTEntityArray[1].IntTestField = 20;
            tesTEntityArray[1].StringsTestField = "String 20";
            tesTEntityArray[2].IntTestField = 30;
            tesTEntityArray[2].StringsTestField = "String 30";
            await Task.Run(async () => await serviceRepository1.UpdateAsync(tesTEntityArray));

            var filter1 = Builders<TesTEntityForUpdateTest>.Filter.Eq("Id", tesTEntityArray[0].Id);
            var entity1 = mongoDbContextService.DbContext.GetCollection<TesTEntityForUpdateTest>(serviceRepository1.CollectionName).Find(filter1).FirstOrDefault();
            var filter2 = Builders<TesTEntityForUpdateTest>.Filter.Eq("Id", tesTEntityArray[1].Id);
            var entity2 = mongoDbContextService.DbContext.GetCollection<TesTEntityForUpdateTest>(serviceRepository1.CollectionName).Find(filter2).FirstOrDefault();
            var filter3 = Builders<TesTEntityForUpdateTest>.Filter.Eq("Id", tesTEntityArray[2].Id);
            var entity3 = mongoDbContextService.DbContext.GetCollection<TesTEntityForUpdateTest>(serviceRepository1.CollectionName).Find(filter3).FirstOrDefault();
            var countInCollection = await mongoDbContextService.DbContext.GetCollection<TesTEntityForUpdateTest>(serviceRepository1.CollectionName).CountAsync(x => true);

            // Assert
            // Assert
            Assert.Equal(3, countInCollection);

            Assert.NotNull(entity1);
            Assert.Equal(entity1.Id, tesTEntityArray[0].Id);
            Assert.Equal(10, entity1.IntTestField);
            Assert.Equal("String 10", entity1.StringsTestField);

            Assert.NotNull(entity2);
            Assert.Equal(entity2.Id, tesTEntityArray[1].Id);
            Assert.Equal(20, entity2.IntTestField);
            Assert.Equal("String 20", entity2.StringsTestField);

            Assert.NotNull(entity3);
            Assert.Equal(entity3.Id, tesTEntityArray[2].Id);
            Assert.Equal(30, entity3.IntTestField);
            Assert.Equal("String 30", entity3.StringsTestField);

            // Drop collection
            await DropCollectionAsync(mongoDbContextService, serviceRepository1.CollectionName);
        }

        [Fact]
        public async Task UpdateAsync_IEnumerable_Entity()
        {
            // Arrange
            var serviceRepository1 = (IRepository<TesTEntityForUpdateTest>)_testServiceProvider.GetService(typeof(IRepository<TesTEntityForUpdateTest>));
            // Drop collection
            var mongoDbContextService = (IMongoDbContext)_testServiceProvider.GetService(typeof(IMongoDbContext));
            await DropCollectionAsync(mongoDbContextService, serviceRepository1.CollectionName);

            var tesTEntityArray = new List<TesTEntityForUpdateTest>
            {
                new TesTEntityForUpdateTest { IntTestField = 1, StringsTestField = "String 1", Id = new ObjectId() },
                new TesTEntityForUpdateTest { IntTestField = 2, StringsTestField = "String 2", Id = new ObjectId() },
                new TesTEntityForUpdateTest { IntTestField = 3, StringsTestField = "String 3", Id = new ObjectId() }
            };
            await Task.Run(async () => await serviceRepository1.InsertAsync(tesTEntityArray));

            // Act
            tesTEntityArray[0].IntTestField = 10;
            tesTEntityArray[0].StringsTestField = "String 10";
            tesTEntityArray[1].IntTestField = 20;
            tesTEntityArray[1].StringsTestField = "String 20";
            tesTEntityArray[2].IntTestField = 30;
            tesTEntityArray[2].StringsTestField = "String 30";
            await Task.Run(async () => await serviceRepository1.UpdateAsync(tesTEntityArray));

            var filter1 = Builders<TesTEntityForUpdateTest>.Filter.Eq("Id", tesTEntityArray[0].Id);
            var entity1 = mongoDbContextService.DbContext.GetCollection<TesTEntityForUpdateTest>(serviceRepository1.CollectionName).Find(filter1).FirstOrDefault();
            var filter2 = Builders<TesTEntityForUpdateTest>.Filter.Eq("Id", tesTEntityArray[1].Id);
            var entity2 = mongoDbContextService.DbContext.GetCollection<TesTEntityForUpdateTest>(serviceRepository1.CollectionName).Find(filter2).FirstOrDefault();
            var filter3 = Builders<TesTEntityForUpdateTest>.Filter.Eq("Id", tesTEntityArray[2].Id);
            var entity3 = mongoDbContextService.DbContext.GetCollection<TesTEntityForUpdateTest>(serviceRepository1.CollectionName).Find(filter3).FirstOrDefault();
            var countInCollection = await mongoDbContextService.DbContext.GetCollection<TesTEntityForUpdateTest>(serviceRepository1.CollectionName).CountAsync(x => true);

            // Assert
            // Assert
            Assert.Equal(3, countInCollection);

            Assert.NotNull(entity1);
            Assert.Equal(entity1.Id, tesTEntityArray[0].Id);
            Assert.Equal(10, entity1.IntTestField);
            Assert.Equal("String 10", entity1.StringsTestField);

            Assert.NotNull(entity2);
            Assert.Equal(entity2.Id, tesTEntityArray[1].Id);
            Assert.Equal(20, entity2.IntTestField);
            Assert.Equal("String 20", entity2.StringsTestField);

            Assert.NotNull(entity3);
            Assert.Equal(entity3.Id, tesTEntityArray[2].Id);
            Assert.Equal(30, entity3.IntTestField);
            Assert.Equal("String 30", entity3.StringsTestField);

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
