using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Api.Web.Configurations
{
    public static class SwaggerExtension
    {
        private static readonly string PodName = Environment.GetEnvironmentVariable("MY_POD_NAME") ?? "Person API";

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.CustomSchemaIds(t => t.FullName);
                options.SwaggerDoc("v1", new OpenApiInfo { Title = PodName, Version = "v1" });
            });

            return services;
        }

        public static IApplicationBuilder UseSwashbuckleSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", PodName);
                options.RoutePrefix = string.Empty;
            });

            return app;
        }
    }
}
