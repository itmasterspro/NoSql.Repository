using System;
using ItMastersPro.NoSql.Repository.MongoDb;
using ItMastersPro.NoSql.Repository.MongoDb.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace NoSql.Repository.MongoDb.IntegrationTests
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.UseMongoDb(Configuration.GetSection("MongoConnection:ConnectionString").Value);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            ;
        }
    }
}
