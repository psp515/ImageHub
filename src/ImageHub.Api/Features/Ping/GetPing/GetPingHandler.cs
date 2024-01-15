using ImageHub.Api.Contracts.Ping.GetPing;
using ImageHub.Api.Features.Ping.Get;

namespace ImageHub.Api.Features.Ping.GetPing;

public class GetPingHandler : IRequestHandler<GetPingQuery, Result<GetPingResponse>>
{
    private static readonly string WelcomeMessage = "Welcome to ImageHub, {0}!";

    public Task<Result<GetPingResponse>> Handle(GetPingQuery query, CancellationToken cancellationToken)
    {
        if(query.Name.Length < 3)
        {
            return Task.FromResult(Result<GetPingResponse>.Failure(GetPingErrors.NameTooShort));
        }

        var response = new GetPingResponse(Guid.NewGuid(),
            string.Format(WelcomeMessage, query.Name),
            DateTime.UtcNow);

        return Task.FromResult(Result<GetPingResponse>.Success(response));
    }
}
