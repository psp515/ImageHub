namespace ImageHub.Api.Features.Ping.Get;

public record GetPingResponse(Guid Id, string Message, DateTime PingAtUtc);
