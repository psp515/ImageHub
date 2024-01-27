namespace ImageHub.Api.Features.Thumbnails.GetThumbnails;

public class GetThumbnailsError
{
    public static Error ThumbnailsNotFound
        => Error.NotFound("Thumbnail.Get.NotFound", "Thumbnails not found.");
}
