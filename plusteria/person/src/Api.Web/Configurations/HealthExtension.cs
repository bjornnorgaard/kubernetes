using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Web.Configurations
{
    public static class HealthExtension
    {
        public static IServiceCollection AddHealth(this IServiceCollection services)
        {
            services.AddHealthChecks();

            return services;
        }

        public static IApplicationBuilder UseHealth(this IApplicationBuilder app)
        {
            app.UseHealthChecks("/health");
            app.UseHealthChecks("/ready");

            return app;
        }
    }
}
