using ImageHub.Api.Infrastructure.Services;

namespace ImageHub.Api.Infrastructure.Behaviors;

internal sealed class CachingBehaviour<TRequest, TResponse>(ICacheService cacheService) 
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ICacheQuery
    where TResponse : Result
{

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        return await cacheService.GetOrCreateAsync(request.Key, 
                       ct => next(), 
                       request.Expiration, 
                       cancellationToken);
    }
}
