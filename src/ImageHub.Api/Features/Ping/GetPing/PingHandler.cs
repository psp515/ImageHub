using ImageHub.Api.Features.Ping.Get;

namespace ImageHub.Api.Features.Ping.GetPing;

public class PingHandler : IRequestHandler<PingQuery, Result<PingResponse>>
{
    private static readonly string WelcomeMessage = "Welcome to ImageHub, {0}!";

    public Task<Result<PingResponse>> Handle(PingQuery query, CancellationToken cancellationToken)
    {
        if(query.Name.Length < 3)
        {
            return Task.FromResult(Result<PingResponse>.Failure(PingErrors.NameTooShort));
        }

        var response = new PingResponse(Guid.NewGuid(),
            string.Format(WelcomeMessage, query.Name),
            DateTime.UtcNow);

        return Task.FromResult(Result<PingResponse>.Success(response));
    }
}
