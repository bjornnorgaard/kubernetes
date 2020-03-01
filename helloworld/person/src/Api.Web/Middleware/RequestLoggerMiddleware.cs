using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Api.Web.Middleware
{
    public class RequestLoggerMiddleware
    {
        private readonly ILogger<RequestLoggerMiddleware> _logger;
        private readonly RequestDelegate _next;

        public RequestLoggerMiddleware(RequestDelegate next,
                                       ILogger<RequestLoggerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var sw = Stopwatch.StartNew();
            context.Items.TryGetValue("CorrelationId", out var correlationId);

            var requestData = new { context.Request.Method, Path = context.Request.Path.Value };
            var template = "Started request {CorrelationId} {@RequestData}";
            _logger.LogInformation(template, correlationId, requestData);

            await _next.Invoke(context);

            var responseData = new
            {
                context.Request.Method, Path = context.Request.Path.Value,
                context.Response.StatusCode
            };

            var level = LogLevel.Information;
            if (399 < responseData.StatusCode) level = LogLevel.Warning;
            if (499 < responseData.StatusCode) level = LogLevel.Error;

            template = "Finished request {CorrelationId} in {Elapsed} ms {@RequestData}";
            _logger.Log(level, template, correlationId, sw.ElapsedMilliseconds, responseData);
        }
    }
}



