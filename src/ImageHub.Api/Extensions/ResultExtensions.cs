using System.ComponentModel;

namespace ImageHub.Api.Extensions;

public static class ResultExtensions
{
    public static IResult ToResultsDetails(this Result result)
    {
        if (result.IsSuccess)
        {
            throw new InvalidOperationException();
        }

        if (result.Error.Type == ErrorType.None)
        {
            throw new InvalidEnumArgumentException();
        }

        var errorType = result.Error.Type;

        return Results.Problem(
            statusCode: GetStatusCode(errorType),
            title: GetTitle(errorType),
            extensions: new Dictionary<string, object?>
            {
                {"errors", new[] { result.Error } }
            });
    }

    private static int GetStatusCode(ErrorType errorType)
        => errorType switch
        {
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Failure => StatusCodes.Status400BadRequest,
            _ => throw new InvalidEnumArgumentException()
        };

    private static string GetTitle(ErrorType errorType)
    => errorType switch
    {
        ErrorType.Validation => "Bad Request",
        ErrorType.NotFound => "Not Found",
        ErrorType.Conflict => "Conflict",
        ErrorType.Failure => "Bad Request",
        _ => throw new InvalidEnumArgumentException()
    };
}
