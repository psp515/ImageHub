using FluentValidation;

namespace ImageHub.Api.Infrastructure.Behaviors;

internal sealed class ValidationBehaviour<TRequest, TResponse>(
    IEnumerable<IValidator<TRequest>> validators,
    ILogger<IPipelineBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : Result
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!validators.Any())
        {
            logger.LogWarning("Request Type: {@RequestName}, Time: {@DateTimeUtc}, Validation skipped.",
                typeof(TRequest).Name,
                DateTime.UtcNow);
            return await next();
        }

        logger.LogInformation("Request Type: {@RequestName}, Time: {@DateTimeUtc}, Starting validation.",
            typeof(TRequest).Name,
            DateTime.UtcNow);

        Error[] errors = validators.Select(validator => validator.Validate(request))
            .SelectMany(validationResult => validationResult.Errors)
            .Where(validationFailure => validationFailure is not null)
            .Select(error => Error.Validation(error.ErrorCode, error.ErrorMessage))
            .Distinct()
            .ToArray();

        if (errors.Length != 0)
        {
            logger.LogError("Request Type: {@RequestName}, Time: {@DateTimeUtc}, Validation failed.",
                typeof(TRequest).Name,
                DateTime.UtcNow);

            return CreateValidationResult<TResponse>(errors);
        }

        logger.LogInformation("Request Type: {@RequestName}, Time: {@DateTimeUtc}, Validation successfull.",
            typeof(TRequest).Name,
            DateTime.UtcNow);

        return await next();
    }

    public static TResult CreateValidationResult<TResult>(Error[] errors)
        where TResult : Result
    {
        if (typeof(TResult) == typeof(Result))
        {
            return (ValidationResult.WithErrors(errors) as TResult)!;
        }

        object validationResult = typeof(ValidationResult<>)!
            .GetGenericTypeDefinition()!
            .MakeGenericType(typeof(TResult).GenericTypeArguments[0])!
            .GetMethod(nameof(ValidationResult.WithErrors))!
            .Invoke(null, [errors])!;

        return (TResult)validationResult;
    }
}
