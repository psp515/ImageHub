using ImageHub.Api.Features.Ping.Get;

namespace ImageHub.Api.Features.Ping.GetPing;

public class PingHandler : IRequestHandler<PingQuery, Result<PingResponse>>
{
    private static string WelcomeMessage = "Welcome to ImageHub, {0}!";

    public async Task<Result<PingResponse>> Handle(PingQuery query, CancellationToken cancellationToken)
    {
        if(query.Name.Length < 3)
        {
            return Result<PingResponse>.Failure(PingErrors.NameTooShort);
        }

        var response = new PingResponse(Guid.NewGuid(),
            string.Format(WelcomeMessage, query.Name),
            DateTime.UtcNow);

        return Result<PingResponse>.Success(response);
    }
}
