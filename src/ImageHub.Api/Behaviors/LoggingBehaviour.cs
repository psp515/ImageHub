namespace ImageHub.Api.Behaviors;

public class LoggingBehaviour<TRequest, TResponse>(ILogger<IPipelineBehavior<TRequest, TResponse>> logger) 
    : IPipelineBehavior<TRequest, TResponse> 
    where TRequest : IRequest<TResponse> 
    where TResponse : Result
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var requestId = Guid.NewGuid();

        logger.LogInformation("Request Id: {requestId}, Request Type: {@RequestName}, Time: {@DateTimeUtc}, Started processing.",
                requestId,
                typeof(TRequest).Name,
                DateTime.UtcNow);

        var response = await next();

        if (response.IsFailure)
        {
            logger.LogError("Request Id: {requestId}, Request Type: {@RequestName} Time: {@DateTimeUtc}, finished processing with error {@Error}",
                requestId,
                typeof(TRequest).Name,
                DateTime.UtcNow,
                response.Error);

            return response;
        }

        logger.LogInformation("Request Id: {requestId}, Request Type: {@RequestName} Time: {@DateTimeUtc}, finished processing with success",
            requestId,
            typeof(TRequest).Name,
            DateTime.UtcNow);

        return response;
    }
}
