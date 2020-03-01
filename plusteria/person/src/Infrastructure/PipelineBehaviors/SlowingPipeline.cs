using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Infrastructure.PipelineBehaviors
{
    public class SlowingPipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<SlowingPipeline<TRequest, TResponse>> _logger;

        public SlowingPipeline(ILogger<SlowingPipeline<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request,
                                            CancellationToken cancellationToken,
                                            RequestHandlerDelegate<TResponse> next)
        {
            var sleepTime = new Random().Next(1, 1000);
            _logger.LogInformation("Applying slow of {SleepTime} ms before processing request", sleepTime);
            Thread.Sleep(sleepTime);
            return await next();
        }
    }
}
