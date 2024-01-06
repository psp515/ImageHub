namespace ImageHub.Api.Shared;

public class Result<T>
{

    private Result(bool isSuccess, Error error)
    {
        if(isSuccess && error != Error.None)
            throw new ArgumentException("Success result cannot have an error", nameof(error));
        if(!isSuccess && error == Error.None)
            throw new ArgumentException("Failure result must have an error", nameof(error));

        IsSuccess = isSuccess;
        Error = error;
        Value = default!;
    }

    private Result(bool isSuccess, Error error, T value)
    {
        if (isSuccess && error != Error.None)
            throw new ArgumentException("Success result cannot have an error", nameof(error));
        if (!isSuccess && error == Error.None)
            throw new ArgumentException("Failure result must have an error", nameof(error));

        IsSuccess = isSuccess;
        Error = error;
        Value = value;
    }

    public T Value { get; }
    public Error Error { get; }
    public bool IsSuccess { get; }

    public bool IsFailure => !IsSuccess;

    public static Result<T> Success(T value) => new(true, Error.None, value);
    public static Result<T> Failure(Error error) => new(false, error);
}
