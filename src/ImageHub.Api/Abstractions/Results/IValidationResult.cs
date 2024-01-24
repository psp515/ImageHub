namespace ImageHub.Api.Abstractions.Results;

public interface IValidationResult 
{
    public static readonly Error ValidationError 
        = Error.Validation("Validation.Error", "Validation failed.");

    Error[] ValidationErrors { get; }
}
