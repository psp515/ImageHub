namespace ImageHub.Api.Contracts.Ping.GetPing;

public record GetPingResponse(Guid Id, string Message, DateTime PingAtUtc);
