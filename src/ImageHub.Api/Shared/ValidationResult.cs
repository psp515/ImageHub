using ImageHub.Api.Abstractions.Results;

namespace ImageHub.Api.Shared;

public class ValidationResult : Result, IValidationResult
{
    private ValidationResult(Error[] errors) : base(false, IValidationResult.ValidationError)
    {
        ValidationErrors = errors;
    }
    public Error[] ValidationErrors { get; }

    public static ValidationResult WithErrors(Error[] errors) => new(errors);
}

public class ValidationResult<T> : Result<T>, IValidationResult
{
    private ValidationResult(Error[] errors) : base(false, IValidationResult.ValidationError)
    {
        ValidationErrors = errors;
    }

    public Error[] ValidationErrors { get; }

    public static ValidationResult<T> WithErrors(Error[] errors) => new(errors);
}
