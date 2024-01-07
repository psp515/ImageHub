

using FluentValidation.Results;

namespace ImageHub.Api.Features.ImagePacks.AddImagePack;

public class AddImagePackErrors
{
    public static Error ValidationFailed(ValidationResult result) 
        => new("ImagePack.Add.Validation", result.ToString());

    public static Error ImagePackExist
        => new("ImagePack.Add.Exists");
}
