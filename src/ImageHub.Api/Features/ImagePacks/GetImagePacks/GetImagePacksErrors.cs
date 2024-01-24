namespace ImageHub.Api.Features.ImagePacks.GetImagePacks;

public class GetImagePacksErrors
{
    public static Error ImagePacksNotFound
        => Error.NotFound("ImagePacks.Get.NotFound", "no more imagepacks.");
}
