namespace ImageHub.Api.Features.Ping.GetPing;

public class GetPingErrors
{
    public static Error NameTooShort 
        = Error.Validation("Ping.NameTooShort", "Name must be at least 3 characters long.");
}
