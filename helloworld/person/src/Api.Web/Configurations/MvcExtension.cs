using Api.Web.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Web.Configurations
{
    public static class MvcExtension
    {
        public static IMvcBuilder AddControllersWithFilters(this IServiceCollection services)
        {
            return services.AddControllers(o => { o.Filters.Add<ExceptionFilter>(); })
                           .AddApplicationPart(typeof(Startup).Assembly);
        }
    }
}



