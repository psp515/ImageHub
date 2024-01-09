namespace ImageHub.Api.Features.Ping.Get;

public class GetPingQuery : IRequest<Result<GetPingResponse>>
{
    public string Name { get; set; } = string.Empty;
}
