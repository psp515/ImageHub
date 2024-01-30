using System.Diagnostics;

namespace ImageHub.Api.Infrastructure.Middlewares;

public class TimingMiddleware(ILogger<TimingMiddleware> logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var stopwatch = Stopwatch.StartNew();

        context.Response.OnStarting(() =>
        {
            stopwatch.Stop();
            logger.LogInformation("Request {method} {path} executed total of: {time} ms.", context.Request.Method, context.Request.Path, stopwatch.ElapsedMilliseconds);
            context.Response.Headers.Append("Response-Time", stopwatch.ElapsedMilliseconds.ToString());
            return Task.CompletedTask;
        });

        await next(context);
    }
}
