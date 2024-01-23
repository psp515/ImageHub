using FluentValidation.Results;

namespace ImageHub.Api.Features.Images.GetImage;

public class GetImageErrors
{
    public static Error ImageNotFound 
        => Error.NotFound("Image.Get.NotFound");
}
