using Api.Web.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Web.Configurations
{
    public static class CorsExtension
    {
        private static readonly string _defaultPolicy = "DefaultCorsPolicy";

        public static IServiceCollection AddCorsPolicy(this IServiceCollection services,
                                                       IConfiguration configuration)
        {
            var corsOptions = new CorsOptions();
            configuration.GetSection(nameof(CorsOptions)).Bind(corsOptions);

            services.AddCors(
                             options =>
                             {
                                 options.AddPolicy(
                                                   _defaultPolicy,
                                                   builder =>
                                                   {
                                                       builder.WithOrigins(corsOptions
                                                                               .AllowedOrigins)
                                                              .AllowAnyHeader()
                                                              .AllowAnyMethod()
                                                              .AllowCredentials();
                                                   });
                             });

            return services;
        }

        public static IApplicationBuilder UseCorsPolicy(this IApplicationBuilder app)
        {
            app.UseCors(_defaultPolicy);

            return app;
        }
    }
}



