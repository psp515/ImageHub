using FluentValidation.Results;

namespace ImageHub.Api.Features.ImagePacks.AddImagePack;

public class UpdateImagePackErrors
{
    public static Error ValidationFailed(ValidationResult result) 
        => Error.Validation("ImagePack.Edit.Validation", result.ToString());

    public static Error NotFound
        => Error.NotFound("ImagePack.Edit.NotFound");
}
