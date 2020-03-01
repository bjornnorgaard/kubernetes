using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Models;
using Repository;
using Serilog;

namespace Api.Web.Configurations
{
    public static class DatabaseExtension
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services,
                                                     IConfiguration configuration)
        {
            services.AddDbContext<Context>(o =>
            {
                o.UseSqlServer(configuration
                                   .GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<IContext>(ctx => ctx.GetService<Context>());

            return services;
        }

        public static IApplicationBuilder UpdateDatabase(this IApplicationBuilder app,
                                                         IWebHostEnvironment env)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<Context>();
                try
                {
                    Log.Logger.Information("Attempting to migrate database");
                    context.Database.Migrate();
                }
                catch (Exception e)
                {
                    Log.Logger.Information("Failed to migrate database with exception: {e}", e);
                }

                Log.Logger.Information("Successfully migrated database");

                if (context.Persons.Any())
                {
                    Log.Logger.Information("Database not empty, continuing without seeding");    
                }
                if (!context.Persons.Any())
                {
                    Log.Logger.Information("Database is empty, seeding started");
                    var pets = new List<Person>
                    {
                        new Person { Name = "Simon Says", Age = 4 },
                        new Person { Name = "John Doe", Age = 15 },
                        new Person { Name = "Anders And", Age = 7 }
                    };
                    context.AddRange(pets);
                    context.SaveChanges();
                }
            }
            
            Log.Logger.Information("Successfully finished database update");
            return app;
        }
    }
}
