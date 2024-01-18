using FluentValidation;
using ImageHub.Api.Contracts.Image.AddImage;
using ImageHub.Api.Features.ImagePacks;
using ImageHub.Api.Features.Images.Repositories;

namespace ImageHub.Api.Features.Images.AddImage;

public class AddImageHandler(IImageRepository repository, 
                             IImageStoreRepository imageStoreRepository,
                             IImagePackRepository imagePackRepository,
                             IValidator<AddImageCommand> validator) 
    : IRequestHandler<AddImageCommand, Result<AddImageResponse>>
{
    public async Task<Result<AddImageResponse>> Handle(AddImageCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            var error = AddImageErrors.ValidationFailed(validationResult);
            return Result<AddImageResponse>.Failure(error);
        }

        var exists = await repository.ExistsByName(request.Name, cancellationToken);

        if (exists)
        {
            var error = AddImageErrors.ImageExist;
            return Result<AddImageResponse>.Failure(error);
        }

        Guid? packId = null;

        if (request.PackId is not null)
        {
            var pack = await imagePackRepository.GetImagePackById((Guid)request.PackId, cancellationToken);

            if (pack is null)
            {
                var error = AddImageErrors.PackIsNotExisting;
                return Result<AddImageResponse>.Failure(error);
            }

            packId = pack.Id;
        }

        var path = await imageStoreRepository.SaveImage(request.Image);

        if (string.IsNullOrEmpty(path))
        {
            var error = AddImageErrors.FailedToSaveFile;
            return Result<AddImageResponse>.Failure(error);
        }

        var image = new Image
        {
            Id = new Guid(),
            Name = request.Name,
            Description = request.Description,
            FileType = request.FileType,
            EditedAtUtc = DateTime.UtcNow,
            CreatedOnUtc = DateTime.UtcNow,
            ImageStoreKey = path,
            PackId = packId
        };

        await repository.AddImage(image, cancellationToken);

        //TODO: Process image

        return Result<AddImageResponse>.Success(new(image.Id));
    }
}
