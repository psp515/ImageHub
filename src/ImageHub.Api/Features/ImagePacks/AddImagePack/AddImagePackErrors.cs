namespace ImageHub.Api.Features.ImagePacks.AddImagePack;

public class AddImagePackErrors
{
    public static Error ImagePackExist
        => Error.Conflict("ImagePack.Add.Name.Exists");
}
