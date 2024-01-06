

using FluentValidation.Results;

namespace ImageHub.Api.Features.ImagePacks.AddImagePack;

public class AddImagePackErrors
{
    public static Error ValidationFailed(ValidationResult result) 
        => new("ImagePack.Add", result.ToString());
}
