namespace ImageHub.Api.Infrastructure.Behaviors;

internal sealed class LoggingBehaviour<TRequest, TResponse>(ILogger<IPipelineBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : Result
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        logger.LogInformation("Request Type: {@RequestName}, Time: {@DateTimeUtc}, Started processing.",
                typeof(TRequest).Name,
                DateTime.UtcNow);

        var response = await next();

        if (response.IsFailure)
        {
            logger.LogError("Request Type: {@RequestName} Time: {@DateTimeUtc}, finished processing with error {@Error}",
                typeof(TRequest).Name,
                DateTime.UtcNow,
                response.Error);

            return response;
        }

        logger.LogInformation("Request Type: {@RequestName} Time: {@DateTimeUtc}, finished processing with success",
            typeof(TRequest).Name,
            DateTime.UtcNow);

        return response;
    }
}
