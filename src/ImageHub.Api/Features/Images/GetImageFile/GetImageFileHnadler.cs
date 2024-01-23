using ImageHub.Api.Contracts.Image.GetImageFile;
using ImageHub.Api.Features.Images.Repositories;

namespace ImageHub.Api.Features.Images.GetImageFile;

public class GetImageFileHnadler(IImageRepository imageRepository, 
                                  IImageStoreRepository imageStoreRepository) : IRequestHandler<GetImageFileQuery, Result<GetImageFileResponse>>
{
    public async Task<Result<GetImageFileResponse>> Handle(GetImageFileQuery request, CancellationToken cancellationToken)
    {
        var image = await imageRepository.GetImageById(request.Id, cancellationToken);

        if (image is null)
        {
            var error = GetImageFileErrors.ImageNotFoundInDatabase;
            return Result<GetImageFileResponse>.Failure(error);
        }

        var fileKey = image.ImageStoreKey;

        var bytes = await imageStoreRepository.LoadImage(fileKey);

        if (bytes is null || bytes.Length == 0)
        {
            var error = GetImageFileErrors.ImageNotFoundInStorage;
            return Result<GetImageFileResponse>.Failure(error);
        }

        var response = new GetImageFileResponse
        {
            Bytes = bytes,
            FileType = image.FileType,
            EditedAtUtc = image.EditedAtUtc
        };

        return Result<GetImageFileResponse>.Success(response);
    }
}

