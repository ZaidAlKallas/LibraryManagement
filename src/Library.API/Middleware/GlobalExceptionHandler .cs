using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Library.API.Middleware;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    private readonly Dictionary<Type, Func<Exception, ProblemDetails>> _exceptionHandlers;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;

        _exceptionHandlers = new Dictionary<Type, Func<Exception, ProblemDetails>>
        {
            {
                typeof(KeyNotFoundException),
                ex => new ProblemDetails
                {
                    Title = "Resource not found",
                    Status = StatusCodes.Status404NotFound,
                    Detail = ex.Message
                }
            },
            {
                typeof(ValidationException),
                ex => new ProblemDetails
                {
                    Title = "Validation error",
                    Status = StatusCodes.Status400BadRequest,
                    Detail = ex.Message
                }
            },
            {
                typeof(UnauthorizedAccessException),
                ex => new ProblemDetails
                {
                    Title = "Access denied",
                    Status = StatusCodes.Status403Forbidden,
                    Detail = ex.Message
                }
            }
        };
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        _logger.LogError(exception, "Unhandled exception occurred");

        ProblemDetails problemDetails;

        if (_exceptionHandlers.TryGetValue(exception.GetType(), out var handler))
        {
            problemDetails = handler(exception);
        }
        else
        {
            problemDetails = new ProblemDetails
            {
                Title = "An error occurred while processing your request",
                Status = StatusCodes.Status500InternalServerError,
                Detail = exception.Message
            };
        }

        problemDetails.Instance = httpContext.Request.Path;

        httpContext.Response.StatusCode = problemDetails.Status ?? StatusCodes.Status500InternalServerError;
        httpContext.Response.ContentType = "application/problem+json";

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
        return true;
    }
}
