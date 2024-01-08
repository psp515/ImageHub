using FluentValidation.Results;

namespace ImageHub.Api.Features.ImagePacks.AddImagePack;

public class UpdateImagePackErrors
{
    public static Error ValidationFailed(ValidationResult result) 
        => new("ImagePack.Edit.Validation", result.ToString());

    public static Error NotFound
    => new("ImagePack.Edit.NotFound");
}
