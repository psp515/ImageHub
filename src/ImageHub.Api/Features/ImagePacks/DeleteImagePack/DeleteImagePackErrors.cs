namespace ImageHub.Api.Features.ImagePacks.DeleteImagePack;

public class DeleteImagePackErrors
{
    public static Error NotFound 
        => Error.NotFound("ImagePack.Delete.NotFound");
}