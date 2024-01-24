namespace ImageHub.Api.Features.Images.GetImageFile;

public class GetImageFileErrors
{
    public static Error ImageNotFoundInDatabase
        => Error.NotFound("Image.GetFile.NotFound.Database");

    public static Error ImageNotFoundInStorage
        => Error.NotFound("Image.GetFile.NotFound.Storage");
}
