using Api.Web.Middleware;
using Microsoft.AspNetCore.Builder;

namespace Api.Web.Configurations
{
    public static class MiddlewareExtension
    {
        public static IApplicationBuilder UseMiddleware(this IApplicationBuilder app)
        {
            // Person is important!
            app.UseMiddleware<CorrelationMiddleware>();
            app.UseMiddleware<RequestLoggerMiddleware>();
            app.UseMiddleware<SlowRequestLoggerMiddleware>();

            return app;
        }
    }
}



