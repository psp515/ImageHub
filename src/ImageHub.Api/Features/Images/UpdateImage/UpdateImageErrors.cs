namespace ImageHub.Api.Features.Images.UpdateImage;

public class UpdateImageErrors
{
    public static Error ImageNotFound =>
        Error.NotFound("Image.Update.NotFound", "Image not found.");

    public static Error VailedToSaveImage =>
        Error.ServerError("Image.Update.Fail", "Failed to save updates.");
}
