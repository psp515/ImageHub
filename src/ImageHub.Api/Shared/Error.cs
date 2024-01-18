
namespace ImageHub.Api.Shared;

public record Error
{
    public static Error None => new("None", ErrorType.None);

    public string Code { get; }
    public ErrorType Type { get; }
    public string Description { get; }

    private Error(string code, ErrorType type, string? description = null) 
    {
        Code = code;
        Type = type;
        Description = description ?? "No description provided.";
    }

    public Result ToResult() => Result.Failure(this);

    public static Error NotFound(string code, string? description = null)
        => new(code, ErrorType.NotFound, description);
    public static Error Validation(string code, string? description = null)
        => new(code, ErrorType.Validation, description);
    public static Error Conflict(string code, string? description = null)
        => new(code, ErrorType.Conflict, description);
    public static Error Failure(string code, string? description = null)
        => new(code, ErrorType.Failure, description);
    public static Error ServerError(string code, string? description = null) 
        => new(code, ErrorType.ServerFail, description);
}