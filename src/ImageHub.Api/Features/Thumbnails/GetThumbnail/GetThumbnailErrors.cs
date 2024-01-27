namespace ImageHub.Api.Features.Thumbnails.GetThumbnail;

public class GetThumbnailErrors
{
    public static Error NotFound 
        => Error.NotFound("Thumbnail.Get.NotFound");
}
