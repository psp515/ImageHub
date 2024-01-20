using FluentValidation;
using ImageHub.Api.Contracts.Image.GetImageFile;
using ImageHub.Api.Features.Images.Repositories;

namespace ImageHub.Api.Features.Images.GetImageFile;

public class GetImageFileQueryHnadler(IImageRepository imageRepository, 
                                  IImageStoreRepository imageStoreRepository,
                                  IValidator<GetImageFileQuery> validator) : IRequestHandler<GetImageFileQuery, Result<GetImageFileResponse>>
{
    public async Task<Result<GetImageFileResponse>> Handle(GetImageFileQuery request, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            var error = GetImageFileErrors.ValidationFailed(validationResult);
            return Result<GetImageFileResponse>.Failure(error);
        }

        var guid = Guid.Parse(request.Id);
        var image = await imageRepository.GetImageById(guid, cancellationToken);

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

