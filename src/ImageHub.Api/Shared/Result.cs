namespace ImageHub.Api.Shared;

public class Result
{
    protected Result(bool isSuccess, Error error)
    {
        if (isSuccess && error != Error.None)
            throw new ArgumentException("Success result cannot have an error.", nameof(error));
        if (!isSuccess && error == Error.None)
            throw new ArgumentException("Failure result must have an error.", nameof(error));

        IsSuccess = isSuccess;
        Error = error;
    }

    public Error Error { get; }
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;

    public static Result Success() => new(true, Error.None);
    public static Result Failure(Error error) => new(false, error);
}

public class Result<T> : Result
{
    protected Result(bool isSuccess, Error error, T value) : base(isSuccess, error)
    {
        Value = value;
    }

    protected Result(bool isSuccess, Error error) : base(isSuccess, error)
    {
        Value = default!;
    }

    public T Value { get; }

    public static Result<T> Success(T value) => new(true, Error.None, value);
    public static new Result<T> Failure(Error error) => new(false, error);
}
