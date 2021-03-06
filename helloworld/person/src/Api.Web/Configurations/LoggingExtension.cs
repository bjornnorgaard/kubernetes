using System;
using Api.Web.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;

namespace Api.Web.Configurations
{
    public static class LoggingExtension
    {
        public static IServiceCollection AddLogger(this IServiceCollection services,
                                                   IConfiguration configuration)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var podName = Environment.GetEnvironmentVariable("POD_NAME") ?? "Local";
            
            var o = new LoggingOptions();
            configuration.Bind(nameof(LoggingOptions), o);

            var elasticsearchSinkOptions = new ElasticsearchSinkOptions(new Uri(o.ElasticsearchUrl))
            {
                AutoRegisterTemplate = true,
                IndexFormat = $"logstash-{DateTime.Now:yyyy.MM.dd}",
                ModifyConnectionSettings = x => x.BasicAuthentication("elastic", "changeme")
            };
            
            Log.Logger = new LoggerConfiguration()
                         .ReadFrom.Configuration(configuration)
                         .Enrich.WithProperty("environment", environment)
                         .Enrich.WithProperty("system", o.SystemName)
                         .Enrich.WithProperty("pod", podName)
                         .Enrich.WithExceptionDetails()
                         .Enrich.FromLogContext()
                         .WriteTo.Elasticsearch(elasticsearchSinkOptions)
                         .WriteTo.RollingFile($"C:/logs/{o.SystemName}/{o.SystemName}-{environment}-{{Date}}.txt")
                         .WriteTo.Console()
                         .CreateLogger();

            Log.Logger.Information("Successfully configured logger");

            return services;
        }
    }
}



