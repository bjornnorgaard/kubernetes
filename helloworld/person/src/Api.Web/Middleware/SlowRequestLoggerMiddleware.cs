using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Api.Web.Middleware
{
    public class SlowRequestLoggerMiddleware
    {
        private readonly ILogger<SlowRequestLoggerMiddleware> _logger;
        private readonly RequestDelegate _next;

        public SlowRequestLoggerMiddleware(RequestDelegate next,
                                           ILogger<SlowRequestLoggerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var sw = Stopwatch.StartNew();
            context.Items.TryGetValue("CorrelationId", out var correlationId);

            await _next.Invoke(context);

            if (sw.ElapsedMilliseconds > 500)
            {
                var data = new
                {
                    Path = context.Request.Path.Value, context.Response.StatusCode,
                    Elapsed = sw.ElapsedMilliseconds
                };

                var template =
                    "Request {CorrelationId} was slow to respond ({Elapsed} ms) {@SlowRequestData}";
                _logger.LogWarning(template, correlationId, sw.ElapsedMilliseconds, data);
            }
        }
    }
}



