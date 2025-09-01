using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Middleware;

public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger = logger;

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        _logger.LogError(exception, "Unhandled exception occurred");

        ProblemDetails problemDetails = exception switch
        {
            KeyNotFoundException ex => new ProblemDetails
            {
                Title = "Resource not found",
                Status = StatusCodes.Status404NotFound,
                Detail = ex.Message
            },
            ValidationException ex => new ProblemDetails
            {
                Title = "Validation error",
                Status = StatusCodes.Status400BadRequest,
                Detail = "One or more validation errors occurred.",
                Extensions =
                    {
                        ["errors"] = ex.Errors
                            .Select(e => new { e.PropertyName, e.ErrorMessage })
                            .GroupBy(e => e.PropertyName)
                            .ToDictionary(
                                g => g.Key,
                                g => g.Select(e => e.ErrorMessage).ToArray()
                            )
                    }
            },
            UnauthorizedAccessException ex => new ProblemDetails
            {
                Title = "Access denied",
                Status = StatusCodes.Status403Forbidden,
                Detail = ex.Message
            },
            _ => new ProblemDetails
            {
                Title = "An error occurred while processing your request",
                Status = StatusCodes.Status500InternalServerError,
                Detail = exception.Message
            },
        };
        problemDetails.Instance = httpContext.Request.Path;

        httpContext.Response.StatusCode = problemDetails.Status ?? StatusCodes.Status500InternalServerError;
        httpContext.Response.ContentType = "application/problem+json";

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
        return true;
    }
}
