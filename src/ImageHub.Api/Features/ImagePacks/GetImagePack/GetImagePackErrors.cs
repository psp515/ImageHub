
using FluentValidation.Results;

namespace ImageHub.Api.Features.ImagePacks.GetImagePack;

public class GetImagePackErrors
{
    public static Error ImagePackNotFound 
        => Error.NotFound("ImagePack.Get.NotFound");

    public static Error ValidationFailed(FluentValidation.Results.ValidationResult validationResult)
        => Error.Validation("ImagePack.Get.Validation", validationResult.ToString());
}
