namespace ImageHub.Api.Features.Ping.Get;

public record PingResponse(Guid Id, string Message, DateTime PingAtUtc);
