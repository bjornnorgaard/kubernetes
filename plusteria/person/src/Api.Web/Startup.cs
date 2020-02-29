using Api.Web.Configurations;
using Application;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Api.Web
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
            services.AddControllersWithFilters();
            services.AddAutoMapper(typeof(AssemblyAnchor), typeof(Startup));
            services.AddCorsPolicy(Configuration);
            services.AddMediatrPipeline();
            services.AddHealthChecks();
            services.AddSwagger();

            AddDatabase(services);
            AddLogger(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseRouting();
            app.UseMiddleware();
            app.UseCorsPolicy();
            UpdateDatabase(app, env);
            app.UseSwashbuckleSwagger();
            app.UseHealthChecks("/health");
            app.UseHealthChecks("/ready");
            app.UseEndpoints(e => e.MapControllers());
        }

        /// <summary>
        ///     Overwritten under testing.
        /// </summary>
        public virtual void UpdateDatabase(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UpdateDatabase(env);
        }

        /// <summary>
        ///     Overwritten under testing.
        /// </summary>
        public virtual void AddLogger(IServiceCollection services)
        {
            services.AddLogger(Configuration);
        }

        /// <summary>
        ///     Overwritten under testing.
        /// </summary>
        public virtual void AddDatabase(IServiceCollection services)
        {
            services.AddDatabase(Configuration);
        }
    }
}



