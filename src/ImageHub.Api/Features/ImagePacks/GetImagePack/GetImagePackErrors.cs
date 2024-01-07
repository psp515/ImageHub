
namespace ImageHub.Api.Features.ImagePacks.GetImagePack;

public class GetImagePackErrors
{
    public static Error ImagePackNotFound => new("ImagePack.Get.NotFound");
}
