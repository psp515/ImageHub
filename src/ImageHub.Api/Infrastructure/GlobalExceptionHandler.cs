using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace ImageHub.Api.Infrastructure;

public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        logger.LogError("Time: {@DateTimeUtc}, Exception: {@ExceptioName}, Exception Message: {ExceptionMessage}",
            DateTime.UtcNow,
            nameof(exception),
            exception.Message);

        var problemDetails = new ProblemDetails
        {
            Title = "Server Error",
            Status = StatusCodes.Status500InternalServerError,
            Detail = exception.Message
        };

        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}
