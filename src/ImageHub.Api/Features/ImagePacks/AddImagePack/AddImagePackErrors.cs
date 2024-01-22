using FluentValidation.Results;

namespace ImageHub.Api.Features.ImagePacks.AddImagePack;

public class AddImagePackErrors
{
    public static Error ValidationFailed(FluentValidation.Results.ValidationResult result) 
        => Error.Validation("ImagePack.Add.Validation", result.ToString());

    public static Error ImagePackExist
        => Error.Conflict("ImagePack.Add.Name.Exists");
}
