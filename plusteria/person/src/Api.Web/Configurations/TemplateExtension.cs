using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Web.Configurations
{
    /// <summary>
    ///     Copy me to create configuration by extending.
    ///     Delete unused methods.
    /// </summary>
    public static class TemplateExtension
    {
        public static IServiceCollection AddService(this IServiceCollection services)
        {
            return services;
        }

        public static IApplicationBuilder UseService(this IApplicationBuilder app)
        {
            return app;
        }
    }
}



