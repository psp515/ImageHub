using ImageHub.Api.Contracts.Image.AddImage;
using ImageHub.Api.Features.ImagePacks;
using ImageHub.Api.Features.Images.Repositories;
using ImageHub.Api.Features.Thumbnails;
using ImageHub.Api.Infrastructure.Persistence;

namespace ImageHub.Api.Features.Images.AddImage;

public class AddImageHandler(IImageRepository repository, 
                             IImageStoreRepository imageStoreRepository,
                             IImagePackRepository imagePackRepository,
                             IThumbnailRepository thumbnailRepository,
                             IEventBus eventBus,
                             ApplicationDbContext dbContext) 
    : IRequestHandler<AddImageCommand, Result<AddImageResponse>>
{

    public async Task<Result<AddImageResponse>> Handle(AddImageCommand request, CancellationToken cancellationToken)
    {
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

        using var transaction = dbContext.Database.BeginTransaction();

        try
        {
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

            var status = await repository.AddImage(image, cancellationToken);

            if (status < 1)
            {
                transaction.Rollback();
                var error = AddImageErrors.TransactionFailed("Image could not be saved.");
                return Result<AddImageResponse>.Failure(error);
            }

            var thumbnail = new Thumbnail
            {
                Id = Guid.NewGuid(),
                ImageId = image.Id,
                Image = image,
                ProcessingStatus = ProcessingStatus.NotStarted,
                Bytes = [],
                FileExtension = ThumbnailExtensions.ThumbnailExtension,
                CreatedOnUtc = DateTime.UtcNow,
                EditedAtUtc = DateTime.UtcNow
            };

            var thumbnailStatus = await thumbnailRepository.AddThumbnailBasedOnImage(thumbnail, cancellationToken);

            if (thumbnailStatus < 1)
            {
                transaction.Rollback();
                var error = AddImageErrors.TransactionFailed("Thumbnail could not be saved.");
                return Result<AddImageResponse>.Failure(error);
            }

            transaction.Commit();

            await eventBus.Publish(new AddImageEvent(thumbnail.Id, image.ImageStoreKey), cancellationToken);

            return Result<AddImageResponse>.Success(new(image.Id, thumbnail.Id));
        }
        catch (Exception e)
        {
            transaction.Rollback();
            var error = AddImageErrors.TransactionFailed(e.Message);
            return Result<AddImageResponse>.Failure(error);
        }
    }
}
