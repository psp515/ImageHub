using FluentValidation.Results;

namespace ImageHub.Api.Features.ImagePacks.AddImagePack;

public class EditImagePackErrors
{
    public static Error ValidationFailed(ValidationResult result) 
        => new("ImagePack.Edit.Validation", result.ToString());
}
