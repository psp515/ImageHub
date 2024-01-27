using ImageHub.Api.Contracts.Image.AddImage;
using ImageHub.Api.Entities;
using ImageHub.Api.Features.ImagePacks;
using ImageHub.Api.Features.Images.Repositories;
using ImageHub.Api.Features.Thumbnails;
using ImageHub.Api.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Transactions;

namespace ImageHub.Api.Features.Images.AddImage;

public class AddImageHandler(IImageRepository repository, 
                             IImageStoreRepository imageStoreRepository,
                             IImagePackRepository imagePackRepository,
                             IThumbnailRepository thumbnailRepository,
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
                var error = AddImageErrors.TransactionFailed;
                return Result<AddImageResponse>.Failure(error);
            }

            var thumbnail = await thumbnailRepository.AddThumbnailBasedOnImage(image, cancellationToken);

            if (thumbnail is null)
            {
                transaction.Rollback();
                var error = AddImageErrors.TransactionFailed;
                return Result<AddImageResponse>.Failure(error);
            }

            transaction.Commit();

            return Result<AddImageResponse>.Success(new(image.Id, thumbnail.Id));
        }
        catch (Exception)
        {
            transaction.Rollback();
            var error = AddImageErrors.TransactionFailed;
            return Result<AddImageResponse>.Failure(error);
        }
    }
}
