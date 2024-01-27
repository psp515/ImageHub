namespace ImageHub.Api.Features.Images.AddImage;

public class AddImageErrors
{
    public static Error TransactionFailed 
        => Error.ServerError("Image.Add.Save");

    public static Error ValidationFailed(FluentValidation.Results.ValidationResult result)
        => Error.Validation("Image.Add.Validation", result.ToString());

    public static Error ImageExist
        => Error.Conflict("Image.Add.Name.Exists");

    public static Error FailedToSaveFile
        => Error.ServerError("Image.Add.Store");

    public static Error PackIsNotExisting 
        => Error.Conflict("Image.Add.ImagePack.DontExists");
}
