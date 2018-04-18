using System;
using System.Collections.Generic;
using System.Text;
using ItMastersPro.NoSql.Repository.MongoDb;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace NoSql.Repository.MongoDb.UnitTests
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
            var ttt = Configuration.GetSection("MongoConnection:ConnectionString").Value;
            services.UseMongoDb(ttt);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            ;
        }
    }
}
