using FluentValidation.Results;

namespace ImageHub.Api.Features.Images.DeleteImage;

public class DeleteImageErrors
{
    public static Error ImageNotFound
        => Error.NotFound("Image.Delete.NotFound");

    public static Error ValidationFailed(FluentValidation.Results.ValidationResult validationResult)
        => Error.Validation("Image.Delete.Validation", validationResult.ToString());
}
