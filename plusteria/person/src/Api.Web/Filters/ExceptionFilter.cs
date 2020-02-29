using System;
using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Infrastructure.Exceptions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Api.Web.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        private readonly IWebHostEnvironment _environment;
        private readonly ILogger<ExceptionFilter> _logger;

        public ExceptionFilter(IWebHostEnvironment environment, ILogger<ExceptionFilter> logger)
        {
            _environment = environment;
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            switch (context.Exception)
            {
                case NotFoundException e:
                    if (_environment.IsDevelopment())
                        context.Result = new NotFoundObjectResult(e.Message);
                    else
                        context.Result = new NotFoundResult();

                    break;

                case ValidationException e:
                    var validationResult = new ValidationResult(e.Errors);
                    validationResult.AddToModelState(context.ModelState, null);
                    context.Result = new BadRequestObjectResult(context.ModelState);

                    break;

                case Exception e:
                    _logger.LogError(e, "Unknown exception thrown");

                    if (_environment.IsProduction())
                        context.Result =
                            new BadRequestObjectResult("An error occured. Check the logs.");
                    else
                        context.Result = new BadRequestObjectResult(
                                                                    new
                                                                    {
                                                                        Message =
                                                                            "Unknown exception thrown. Check the logs.",
                                                                        ExceptionMessage = e.Message
                                                                    });

                    break;
            }
        }
    }
}



