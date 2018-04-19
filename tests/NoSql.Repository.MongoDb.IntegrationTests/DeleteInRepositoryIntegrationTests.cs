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
    public class TesTEntityForDeleteTest : IEntity
    {
        public object Id { get; set; }

        public string StringsTestField { get; set; }

        public int IntTestField { get; set; }
    }

    public class DeleteInRepositoryIntegrationTests
    {
        private readonly IServiceProvider _testServiceProvider;

        public DeleteInRepositoryIntegrationTests()
        {
            // Arrange
            _testServiceProvider = WebHost.CreateDefaultBuilder().UseStartup<Startup>().Build().Services;
        }

        [Fact]
        public void Delete_ById_Entity()
        {
            // Arrange
            var mongoDbContextService = (IMongoDbContext)_testServiceProvider.GetService(typeof(IMongoDbContext));
            var serviceRepository1 = (IRepository<TesTEntityForDeleteTest>)_testServiceProvider.GetService(typeof(IRepository<TesTEntityForDeleteTest>));
            DropCollection(mongoDbContextService, serviceRepository1.CollectionName);

            var tesTEntityArray = new[]
            {
                new TesTEntityForDeleteTest { IntTestField = 1, StringsTestField = "String 1", Id = new ObjectId() },
                new TesTEntityForDeleteTest { IntTestField = 2, StringsTestField = "String 2", Id = new ObjectId() },
                new TesTEntityForDeleteTest { IntTestField = 3, StringsTestField = "String 3", Id = new ObjectId() }
            };
            serviceRepository1.Insert(tesTEntityArray);

            // Act
            serviceRepository1.Delete(tesTEntityArray[0].Id);

            var filter1 = Builders<TesTEntityForDeleteTest>.Filter.Eq("Id", tesTEntityArray[0].Id);
            var entity1 = mongoDbContextService.DbContext.GetCollection<TesTEntityForDeleteTest>(serviceRepository1.CollectionName).Find(filter1).FirstOrDefault();

            // Assert
            Assert.Null(entity1);

            // Drop collection
            DropCollection(mongoDbContextService, serviceRepository1.CollectionName);
        }

        [Fact]
        public void Delete_ById_Id_Not_ObjectId_Entity()
        {
            // Arrange
            var mongoDbContextService = (IMongoDbContext)_testServiceProvider.GetService(typeof(IMongoDbContext));
            var serviceRepository1 = (IRepository<TesTEntityForDeleteTest>)_testServiceProvider.GetService(typeof(IRepository<TesTEntityForDeleteTest>));
            DropCollection(mongoDbContextService, serviceRepository1.CollectionName);

            var tesTEntityArray = new[]
            {
                new TesTEntityForDeleteTest { IntTestField = 1, StringsTestField = "String 1", Id = new ObjectId() },
                new TesTEntityForDeleteTest { IntTestField = 2, StringsTestField = "String 2", Id = new ObjectId() },
                new TesTEntityForDeleteTest { IntTestField = 3, StringsTestField = "String 3", Id = new ObjectId() }
            };
            serviceRepository1.Insert(tesTEntityArray);

            // Act
            serviceRepository1.Delete("Id can't parsed to ObjectId");

            var filter1 = Builders<TesTEntityForDeleteTest>.Filter.Eq("Id", tesTEntityArray[0].Id);
            var entity1 = mongoDbContextService.DbContext.GetCollection<TesTEntityForDeleteTest>(serviceRepository1.CollectionName).Find(filter1).FirstOrDefault();

            // Assert
            Assert.NotNull(entity1);

            // Drop collection
            DropCollection(mongoDbContextService, serviceRepository1.CollectionName);
        }

        [Fact]
        public void Delete_One_Entity()
        {
            // Arrange
            var mongoDbContextService = (IMongoDbContext)_testServiceProvider.GetService(typeof(IMongoDbContext));
            var serviceRepository1 = (IRepository<TesTEntityForDeleteTest>)_testServiceProvider.GetService(typeof(IRepository<TesTEntityForDeleteTest>));
            DropCollection(mongoDbContextService, serviceRepository1.CollectionName);

            var tesTEntityArray = new[]
            {
                new TesTEntityForDeleteTest { IntTestField = 1, StringsTestField = "String 1", Id = new ObjectId() },
                new TesTEntityForDeleteTest { IntTestField = 2, StringsTestField = "String 2", Id = new ObjectId() },
                new TesTEntityForDeleteTest { IntTestField = 3, StringsTestField = "String 3", Id = new ObjectId() }
            };
            serviceRepository1.Insert(tesTEntityArray);

            // Act
            serviceRepository1.Delete(tesTEntityArray[0]);

            var filter1 = Builders<TesTEntityForDeleteTest>.Filter.Eq("Id", tesTEntityArray[0].Id);
            var entity1 = mongoDbContextService.DbContext.GetCollection<TesTEntityForDeleteTest>(serviceRepository1.CollectionName).Find(filter1).FirstOrDefault();

            // Assert
            Assert.Null(entity1);

            // Drop collection
            DropCollection(mongoDbContextService, serviceRepository1.CollectionName);
        }

        [Fact]
        public void Delete_Array_Entity()
        {
            // Arrange
            var mongoDbContextService = (IMongoDbContext)_testServiceProvider.GetService(typeof(IMongoDbContext));
            var serviceRepository1 = (IRepository<TesTEntityForDeleteTest>)_testServiceProvider.GetService(typeof(IRepository<TesTEntityForDeleteTest>));
            DropCollection(mongoDbContextService, serviceRepository1.CollectionName);

            var tesTEntityArray = new[]
            {
                new TesTEntityForDeleteTest { IntTestField = 1, StringsTestField = "String 1", Id = new ObjectId() },
                new TesTEntityForDeleteTest { IntTestField = 2, StringsTestField = "String 2", Id = new ObjectId() },
                new TesTEntityForDeleteTest { IntTestField = 3, StringsTestField = "String 3", Id = new ObjectId() }
            };
            serviceRepository1.Insert(tesTEntityArray);

            // Act
            var deleteEntityArray = new[] { tesTEntityArray[0], tesTEntityArray[1] };
            serviceRepository1.Delete(deleteEntityArray);

            var filter1 = Builders<TesTEntityForDeleteTest>.Filter.Eq("Id", tesTEntityArray[0].Id);
            var entity1 = mongoDbContextService.DbContext.GetCollection<TesTEntityForDeleteTest>(serviceRepository1.CollectionName).Find(filter1).FirstOrDefault();
            var filter2 = Builders<TesTEntityForDeleteTest>.Filter.Eq("Id", tesTEntityArray[1].Id);
            var entity2 = mongoDbContextService.DbContext.GetCollection<TesTEntityForDeleteTest>(serviceRepository1.CollectionName).Find(filter2).FirstOrDefault();

            // Assert
            Assert.Null(entity1);
            Assert.Null(entity2);

            // Drop collection
            DropCollection(mongoDbContextService, serviceRepository1.CollectionName);
        }

        [Fact]
        public void Delete_IEnumerable_Entity()
        {
            // Arrange
            var mongoDbContextService = (IMongoDbContext)_testServiceProvider.GetService(typeof(IMongoDbContext));
            var serviceRepository1 = (IRepository<TesTEntityForDeleteTest>)_testServiceProvider.GetService(typeof(IRepository<TesTEntityForDeleteTest>));
            DropCollection(mongoDbContextService, serviceRepository1.CollectionName);

            var tesTEntityArray = new List<TesTEntityForDeleteTest>
            {
                new TesTEntityForDeleteTest { IntTestField = 1, StringsTestField = "String 1", Id = new ObjectId() },
                new TesTEntityForDeleteTest { IntTestField = 2, StringsTestField = "String 2", Id = new ObjectId() },
                new TesTEntityForDeleteTest { IntTestField = 3, StringsTestField = "String 3", Id = new ObjectId() }
            };
            serviceRepository1.Insert(tesTEntityArray);

            // Act
            var deleteEntityIEnumerable = new List<TesTEntityForDeleteTest> { tesTEntityArray[0], tesTEntityArray[1] };
            serviceRepository1.Delete(deleteEntityIEnumerable);


            var filter1 = Builders<TesTEntityForDeleteTest>.Filter.Eq("Id", tesTEntityArray[0].Id);
            var entity1 = mongoDbContextService.DbContext.GetCollection<TesTEntityForDeleteTest>(serviceRepository1.CollectionName).Find(filter1).FirstOrDefault();
            var filter2 = Builders<TesTEntityForDeleteTest>.Filter.Eq("Id", tesTEntityArray[1].Id);
            var entity2 = mongoDbContextService.DbContext.GetCollection<TesTEntityForDeleteTest>(serviceRepository1.CollectionName).Find(filter2).FirstOrDefault();

            // Assert
            Assert.Null(entity1);
            Assert.Null(entity2);

            // Drop collection
            DropCollection(mongoDbContextService, serviceRepository1.CollectionName);
        }

        [Fact]
        public async Task DeleteAsync_ById_Entity()
        {
            // Arrange
            var mongoDbContextService = (IMongoDbContext)_testServiceProvider.GetService(typeof(IMongoDbContext));
            var serviceRepository1 = (IRepository<TesTEntityForDeleteTest>)_testServiceProvider.GetService(typeof(IRepository<TesTEntityForDeleteTest>));
            await DropCollectionAsync(mongoDbContextService, serviceRepository1.CollectionName);

            var tesTEntityArray = new[]
            {
                new TesTEntityForDeleteTest { IntTestField = 1, StringsTestField = "String 1", Id = new ObjectId() },
                new TesTEntityForDeleteTest { IntTestField = 2, StringsTestField = "String 2", Id = new ObjectId() },
                new TesTEntityForDeleteTest { IntTestField = 3, StringsTestField = "String 3", Id = new ObjectId() }
            };
            await Task.Run(async () => await serviceRepository1.InsertAsync(tesTEntityArray));

            // Act
            await Task.Run(async () => await serviceRepository1.DeleteAsync(tesTEntityArray[0].Id));

            var filter1 = Builders<TesTEntityForDeleteTest>.Filter.Eq("Id", tesTEntityArray[0].Id);
            var entity1 = mongoDbContextService.DbContext.GetCollection<TesTEntityForDeleteTest>(serviceRepository1.CollectionName).Find(filter1).FirstOrDefault();

            // Assert
            Assert.Null(entity1);

            // Drop collection
            await DropCollectionAsync(mongoDbContextService, serviceRepository1.CollectionName);
        }

        [Fact]
        public async Task DeleteAsync_ById_Id_Not_ObjectId_Entity()
        {
            // Arrange
            var mongoDbContextService = (IMongoDbContext)_testServiceProvider.GetService(typeof(IMongoDbContext));
            var serviceRepository1 = (IRepository<TesTEntityForDeleteTest>)_testServiceProvider.GetService(typeof(IRepository<TesTEntityForDeleteTest>));
            await DropCollectionAsync(mongoDbContextService, serviceRepository1.CollectionName);

            var tesTEntityArray = new[]
            {
                new TesTEntityForDeleteTest { IntTestField = 1, StringsTestField = "String 1", Id = new ObjectId() },
                new TesTEntityForDeleteTest { IntTestField = 2, StringsTestField = "String 2", Id = new ObjectId() },
                new TesTEntityForDeleteTest { IntTestField = 3, StringsTestField = "String 3", Id = new ObjectId() }
            };
            await Task.Run(async () => await serviceRepository1.InsertAsync(tesTEntityArray));

            // Act
            await Task.Run(async () => await serviceRepository1.DeleteAsync("Id can't parsed to ObjectId"));


            var filter1 = Builders<TesTEntityForDeleteTest>.Filter.Eq("Id", tesTEntityArray[0].Id);
            var entity1 = mongoDbContextService.DbContext.GetCollection<TesTEntityForDeleteTest>(serviceRepository1.CollectionName).Find(filter1).FirstOrDefault();

            // Assert
            Assert.NotNull(entity1);

            // Drop collection
            await DropCollectionAsync(mongoDbContextService, serviceRepository1.CollectionName);
        }

        [Fact]
        public async Task DeleteAsync_One_Entity()
        {
            // Arrange
            var mongoDbContextService = (IMongoDbContext)_testServiceProvider.GetService(typeof(IMongoDbContext));
            var serviceRepository1 = (IRepository<TesTEntityForDeleteTest>)_testServiceProvider.GetService(typeof(IRepository<TesTEntityForDeleteTest>));
            await DropCollectionAsync(mongoDbContextService, serviceRepository1.CollectionName);

            var tesTEntityArray = new[]
            {
                new TesTEntityForDeleteTest { IntTestField = 1, StringsTestField = "String 1", Id = new ObjectId() },
                new TesTEntityForDeleteTest { IntTestField = 2, StringsTestField = "String 2", Id = new ObjectId() },
                new TesTEntityForDeleteTest { IntTestField = 3, StringsTestField = "String 3", Id = new ObjectId() }
            };
            await Task.Run(async () => await serviceRepository1.InsertAsync(tesTEntityArray));

            // Act
            await Task.Run(async () => await serviceRepository1.DeleteAsync(tesTEntityArray[0]));

            var filter1 = Builders<TesTEntityForDeleteTest>.Filter.Eq("Id", tesTEntityArray[0].Id);
            var entity1 = mongoDbContextService.DbContext.GetCollection<TesTEntityForDeleteTest>(serviceRepository1.CollectionName).Find(filter1).FirstOrDefault();

            // Assert
            Assert.Null(entity1);

            // Drop collection
            await DropCollectionAsync(mongoDbContextService, serviceRepository1.CollectionName);
        }

        [Fact]
        public async Task DeleteAsync_Array_Entity()
        {
            // Arrange
            var mongoDbContextService = (IMongoDbContext)_testServiceProvider.GetService(typeof(IMongoDbContext));
            var serviceRepository1 = (IRepository<TesTEntityForDeleteTest>)_testServiceProvider.GetService(typeof(IRepository<TesTEntityForDeleteTest>));
            await DropCollectionAsync(mongoDbContextService, serviceRepository1.CollectionName);

            var tesTEntityArray = new[]
            {
                new TesTEntityForDeleteTest { IntTestField = 1, StringsTestField = "String 1", Id = new ObjectId() },
                new TesTEntityForDeleteTest { IntTestField = 2, StringsTestField = "String 2", Id = new ObjectId() },
                new TesTEntityForDeleteTest { IntTestField = 3, StringsTestField = "String 3", Id = new ObjectId() }
            };
            await Task.Run(async () => await serviceRepository1.InsertAsync(tesTEntityArray));

            // Act
            var deleteEntityArray = new[] { tesTEntityArray[0], tesTEntityArray[1] };
            await Task.Run(async () => await serviceRepository1.DeleteAsync(deleteEntityArray));

            var filter1 = Builders<TesTEntityForDeleteTest>.Filter.Eq("Id", tesTEntityArray[0].Id);
            var entity1 = mongoDbContextService.DbContext.GetCollection<TesTEntityForDeleteTest>(serviceRepository1.CollectionName).Find(filter1).FirstOrDefault();
            var filter2 = Builders<TesTEntityForDeleteTest>.Filter.Eq("Id", tesTEntityArray[1].Id);
            var entity2 = mongoDbContextService.DbContext.GetCollection<TesTEntityForDeleteTest>(serviceRepository1.CollectionName).Find(filter2).FirstOrDefault();

            // Assert
            Assert.Null(entity1);
            Assert.Null(entity2);

            // Drop collection
            await DropCollectionAsync(mongoDbContextService, serviceRepository1.CollectionName);
        }

        [Fact]
        public async Task DeleteAsync_IEnumerable_Entity()
        {
            // Arrange
            var mongoDbContextService = (IMongoDbContext)_testServiceProvider.GetService(typeof(IMongoDbContext));
            var serviceRepository1 = (IRepository<TesTEntityForDeleteTest>)_testServiceProvider.GetService(typeof(IRepository<TesTEntityForDeleteTest>));
            await DropCollectionAsync(mongoDbContextService, serviceRepository1.CollectionName);

            var tesTEntityArray = new List<TesTEntityForDeleteTest>
            {
                new TesTEntityForDeleteTest { IntTestField = 1, StringsTestField = "String 1", Id = new ObjectId() },
                new TesTEntityForDeleteTest { IntTestField = 2, StringsTestField = "String 2", Id = new ObjectId() },
                new TesTEntityForDeleteTest { IntTestField = 3, StringsTestField = "String 3", Id = new ObjectId() }
            };
            await Task.Run(async () => await serviceRepository1.InsertAsync(tesTEntityArray));

            // Act
            var deleteEntityIEnumerable = new List<TesTEntityForDeleteTest> { tesTEntityArray[0], tesTEntityArray[1] };
            await Task.Run(async () => await serviceRepository1.DeleteAsync(deleteEntityIEnumerable));

            var filter1 = Builders<TesTEntityForDeleteTest>.Filter.Eq("Id", tesTEntityArray[0].Id);
            var entity1 = mongoDbContextService.DbContext.GetCollection<TesTEntityForDeleteTest>(serviceRepository1.CollectionName).Find(filter1).FirstOrDefault();
            var filter2 = Builders<TesTEntityForDeleteTest>.Filter.Eq("Id", tesTEntityArray[1].Id);
            var entity2 = mongoDbContextService.DbContext.GetCollection<TesTEntityForDeleteTest>(serviceRepository1.CollectionName).Find(filter2).FirstOrDefault();

            // Assert
            Assert.Null(entity1);
            Assert.Null(entity2);

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
