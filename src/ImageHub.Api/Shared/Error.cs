namespace ImageHub.Api.Shared;

public record Error(string Code, string? Description = null)
{
    public static Error None => new("None");
}
