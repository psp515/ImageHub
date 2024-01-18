using FluentValidation.Results;

namespace ImageHub.Api.Features.Images.GetImage;

public class GetImageErrors
{
    public static Error ImageNotFound 
        => Error.NotFound("Image.Get.NotFound");

    public static Error ValidationFailed(ValidationResult validationResult)
        => Error.Validation("Image.Get.Validation", validationResult.ToString());
}
