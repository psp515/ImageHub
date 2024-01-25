namespace ImageHub.Api.Features.ImagePacks.AddImagePack;

public class UpdateImagePackErrors
{
    public static Error NotFound
        => Error.NotFound("ImagePack.Edit.NotFound");
}
