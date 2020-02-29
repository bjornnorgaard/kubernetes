using Application;
using FluentValidation;
using Infrastructure.PipelineBehaviors;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Web.Configurations
{
    public static class MediatrExtension
    {
        public static IServiceCollection AddMediatrPipeline(this IServiceCollection services)
        {
            services.AddMediatR(typeof(AssemblyAnchor));

            // Person of pipeline-behaviors is important
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingPipeline<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipeline<,>));

            // Add validators
            var validators =
                AssemblyScanner.FindValidatorsInAssemblies(new[]
                {
                    typeof(AssemblyAnchor).Assembly
                });
            validators.ForEach(validator =>
                                   services.AddTransient(validator.InterfaceType,
                                                         validator.ValidatorType));

            return services;
        }
    }
}



