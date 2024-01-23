namespace ImageHub.Api.Features.Images.GetImages;

public class GetImagesError
{
    public static Error ImagesNotFound 
        => Error.NotFound("Image.Get.NotFound", "Images not found.");
}
