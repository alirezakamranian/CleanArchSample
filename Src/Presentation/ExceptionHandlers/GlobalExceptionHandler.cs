using Domain.Common.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;

namespace Presentation.ExceptionHandlers
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(
             HttpContext httpContext,
             Exception exception,
             CancellationToken cancellationToken)
        {

            ProblemDetails problemDetails = CreateProblemDetailFromException(exception);

            if (problemDetails.Status.Value.Equals(500))
                _logger.LogError("Exception occurred: {Message}", exception.Message);
            else
                _logger.LogCritical("Exception occurred: {Message}", exception.Message);

            httpContext.Response.StatusCode = problemDetails.Status!.Value;

            await httpContext.Response
                .WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }

        private static ProblemDetails CreateProblemDetailFromException(Exception exception)
        {
            switch (exception)
            {
                case AccessDeniedException:
                    {
                        return new ProblemDetails
                        {
                            Status = StatusCodes.Status403Forbidden,
                            Title = "AccessDenied",
                            Detail = exception.Message
                        };
                    }

                case DomainException:
                    {
                        return new ProblemDetails
                        {
                            Status = StatusCodes.Status400BadRequest,
                            Title = "BadRequest",
                            Detail = exception.Message
                        };
                    }

                case ValidationException:
                    {
                        return new ProblemDetails
                        {
                            Status = StatusCodes.Status400BadRequest,
                            Title = "InvalidInputParameters",
                            Detail = exception.Message
                        };
                    }

                case not DomainException:
                    {
                        return new ProblemDetails
                        {
                            Status = StatusCodes.Status500InternalServerError,
                            Title = "ServerError",
                            Detail = "ServerError"
                        };
                    }
            }
        }
    }
}
