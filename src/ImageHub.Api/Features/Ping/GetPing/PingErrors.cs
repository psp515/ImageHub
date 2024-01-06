namespace ImageHub.Api.Features.Ping.GetPing;

public class PingErrors
{
    public static Error NameTooShort = new Error("Ping.NameTooShort", "Name must be at least 3 characters long.");
}
