using FluentValidation.Results;

namespace ImageHub.Api.Features.ImagePacks.DeleteImagePack;

public class DeleteImagePackErrors
{
    public static Error ValidationFailed(ValidationResult validationResult)
    {
        return new Error("ImagePack.Delete.Validation", validationResult.ToString());
    }

    public static Error NotFound => new("ImagePack.Delete.NotFound");
}