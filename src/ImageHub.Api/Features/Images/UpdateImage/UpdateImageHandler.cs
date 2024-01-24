using ImageHub.Api.Contracts.Image.UpdateImage;
using ImageHub.Api.Features.Images.Repositories;

namespace ImageHub.Api.Features.Images.UpdateImage;

public class UpdateImageHandler(IImageRepository repository) 
    : IRequestHandler<UpdateImageCommand, Result<UpdateImageResponse>>
{
    public async Task<Result<UpdateImageResponse>> Handle(UpdateImageCommand request, CancellationToken cancellationToken)
    {
        var image = await repository.GetImageById(request.Id, cancellationToken);

        if (image == null)
        {
            var error = UpdateImageErrors.ImageNotFound;
            return Result<UpdateImageResponse>.Failure(error);
        }

        image.Description = request.Description;
        image.EditedAtUtc = DateTime.UtcNow;

        var updated = await repository.UpdateImage(image, cancellationToken);

        if (!updated)
        {
            var error = UpdateImageErrors.VailedToSaveImage;
            return Result<UpdateImageResponse>.Failure(error);
        }

        var response  = new UpdateImageResponse()
        {
            Id = image.Id,
        };

        return Result<UpdateImageResponse>.Success(response);
    }
}
