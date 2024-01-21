using FluentValidation;

namespace ImageHub.Api.Behaviors;

public class ValidationBehaviour<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators
    ,ILogger<IPipelineBehavior<TRequest, TResponse>> logger)
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

            //TODO: fix casting
            var result = ValidationResult<TResponse>(errors);
            return result;
        }

        logger.LogInformation("Request Type: {@RequestName}, Time: {@DateTimeUtc}, Validation successfull.",
            typeof(TRequest).Name,
            DateTime.UtcNow);

        return await next();
    }

    public static TResult ValidationResult<TResult>(Error[] errors) where TResult : Result
    {
        var error = errors.First();
        return (Result.Failure(error) as TResult)!;
    }
}
