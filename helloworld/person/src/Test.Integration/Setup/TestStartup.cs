using Api.Web;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repository;

namespace Test.Integration.Setup
{
    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration) : base(configuration) { }

        public override void AddDatabase(IServiceCollection services)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder
            {
                DataSource = "IntegrationTests", Mode = SqliteOpenMode.Memory,
                Cache = SqliteCacheMode.Shared
            };
            var connectionString = connectionStringBuilder.ToString();
            var connection = new SqliteConnection(connectionString);

            services.AddTransient(
                                  serviceProvider =>
                                  {
                                      var optionsBuilder = new DbContextOptionsBuilder<Context>();
                                      optionsBuilder.UseSqlite(connection);

                                      var dbContext = new Context(optionsBuilder.Options);
                                      dbContext.Database.AutoTransactionsEnabled = false;

                                      return dbContext;
                                  });

            services.AddScoped<IContext>(ctx => ctx.GetService<Context>());
        }

        public override void UpdateDatabase(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Do not seed here under test.
            // Each test must arrange it's own data.
        }

        public override void AddLogger(IServiceCollection services)
        {
            // Not logging under test.
        }
    }
}



