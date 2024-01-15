using FluentValidation.Results;

namespace ImageHub.Api.Features.ImagePacks.DeleteImagePack;

public class DeleteImagePackErrors
{
    public static Error ValidationFailed(ValidationResult validationResult)
        => Error.Validation("ImagePack.Delete.Validation", validationResult.ToString());
    
    public static Error NotFound 
        => Error.NotFound("ImagePack.Delete.NotFound");
}