namespace ImageHub.Api.Behaviors;

public class LoggingBehaviour<TRequest, TResponse>(ILogger<IPipelineBehavior<TRequest, TResponse>> logger) 
    : IPipelineBehavior<TRequest, TResponse> 
    where TRequest : IRequest<TResponse> 
    where TResponse : Result
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var logId = Guid.NewGuid();

        logger.LogInformation("Logging Id: {@LogId}, Request Type: {@RequestName}, Time: {@DateTimeUtc}, Started processing.",
                logId,
                typeof(TRequest).Name,
                DateTime.UtcNow);

        var response = await next();

        if (response.IsFailure)
        {
            logger.LogError("Logging Id: {@LogId}, Request Type: {@RequestName} Time: {@DateTimeUtc}, finished processing with error {@Error}",
                logId,
                typeof(TRequest).Name,
                DateTime.UtcNow,
                response.Error);

            return response;
        }

        logger.LogInformation("Logging Id: {@LogId}, Request Type: {@RequestName} Time: {@DateTimeUtc}, finished processing with success",
            logId,
            typeof(TRequest).Name,
            DateTime.UtcNow);

        return response;
    }
}
