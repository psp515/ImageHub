namespace ImageHub.Api.Features.Ping.Get;

public class PingQuery : IRequest<Result<PingResponse>>
{
    public string Name { get; set; } = string.Empty;
}
