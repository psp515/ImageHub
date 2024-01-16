using FluentValidation.Results;

namespace ImageHub.Api.Features.Images.AddImage;

public class AddImageErrors
{
    public static Error ValidationFailed(ValidationResult result)
        => Error.Validation("Image.Add.Validation", result.ToString());

    public static Error ImagePackExist
        => Error.Conflict("Image.Add.Name.Exists");
}
