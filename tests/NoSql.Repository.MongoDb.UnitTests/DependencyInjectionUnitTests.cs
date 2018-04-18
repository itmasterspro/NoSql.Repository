using System;
using System.Linq;
using ItMastersPro.NoSql.Repository.Interfaces;
using ItMastersPro.NoSql.Repository.MongoDb;
using ItMastersPro.NoSql.Repository.MongoDb.Interfaces;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using NoSql.Repository.MongoDb.UnitTests.Entity;
using Xunit;

namespace NoSql.Repository.MongoDb.UnitTests
{
    public class DependencyInjectionUnitTests
    {
        private readonly IServiceProvider _testServiceProvider;

        public DependencyInjectionUnitTests()
        {
            // Arrange
            _testServiceProvider = WebHost.CreateDefaultBuilder().UseStartup<Startup>().Build().Services;
        }

        [Fact]
        public void Is_MongoDbContext_Injected()
        {
            // Act
            var mongoDbContextService = _testServiceProvider.GetService(typeof(IMongoDbContext));
            var mongoDbContextInterface = mongoDbContextService.GetType().GetInterfaces().Contains(typeof(IMongoDbContext));

            // Assert
            Assert.IsType<MongoDbContext>(mongoDbContextService);
            Assert.True(mongoDbContextInterface);
        }

        [Fact]
        public void Is_Repository_Injected()
        {
            // Act
            var serviceRepository1 = _testServiceProvider.GetService(typeof(IRepository<TesTEntity1>));
            var serviceRepository1Interface = serviceRepository1.GetType().GetInterfaces().Contains(typeof(IMongoDbRepository<TesTEntity1>));

            var serviceRepository2 = _testServiceProvider.GetService(typeof(IRepository<TesTEntity2>));
            var serviceRepository2Interface = serviceRepository2.GetType().GetInterfaces().Contains(typeof(IMongoDbRepository<TesTEntity2>));

            // Assert
            Assert.IsType<MongoDbRepository<TesTEntity1>>(serviceRepository1);
            Assert.True(serviceRepository1Interface);

            Assert.IsAssignableFrom<IRepository<TesTEntity2>>(serviceRepository2);
            Assert.IsType<MongoDbRepository<TesTEntity2>>(serviceRepository2);
            Assert.True(serviceRepository2Interface);
        }

        [Fact]
        public void Is_Repository_Injected_Different_Interfaces()
        {
            // Act
            var serviceRepository1 = _testServiceProvider.GetService(typeof(IRepository<TesTEntity1>));
            var serviceRepositoryInterface = serviceRepository1.GetType().GetInterfaces().Contains(typeof(IMongoDbRepository<TesTEntity2>));

            // Assert
            Assert.IsType<MongoDbRepository<TesTEntity1>>(serviceRepository1);
            Assert.False(serviceRepositoryInterface);
        }
    }
}
