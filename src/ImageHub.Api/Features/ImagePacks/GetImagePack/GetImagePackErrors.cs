namespace ImageHub.Api.Features.ImagePacks.GetImagePack;

public class GetImagePackErrors
{
    public static Error ImagePackNotFound 
        => Error.NotFound("ImagePack.Get.NotFound");
}
