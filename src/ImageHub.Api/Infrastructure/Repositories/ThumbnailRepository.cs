using ImageHub.Api.Features.Thumbnails;
using ImageHub.Api.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ImageHub.Api.Infrastructure.Repositories;

public class ThumbnailRepository(ApplicationDbContext dbContext) : IThumbnailRepository
{
    public static readonly string ThumbnailExtensions = "image/png";

    public async Task<Thumbnail?> AddThumbnailBasedOnImage(Image image, CancellationToken cancellationToken)
    {
        var thumbnail = new Thumbnail
        {
            Id = Guid.NewGuid(),
            ImageId = image.Id,
            Image = image,
            ProcessingStatus = ProcessingStatus.NotStarted,
            Bytes = [],
            FileExtension = ThumbnailExtensions,
            CreatedOnUtc = DateTime.UtcNow,
            EditedAtUtc = DateTime.UtcNow
        };

        var status = await dbContext.Thumbnails.AddAsync(thumbnail, cancellationToken);

        return status.Entity;
    }

    public async Task<Thumbnail?> GetThumbnail(Guid id, CancellationToken cancellationToken)
        => await dbContext.Thumbnails
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public async Task<List<Thumbnail>> GetThumbnails(Guid? imagePackId, int page, int size, CancellationToken cancellationToken)
        => await dbContext.Thumbnails
            .Where(x => x.Image.PackId == imagePackId)
            .Skip((page-1)*size)
            .Take(size)
            .ToListAsync(cancellationToken);

    public async Task<int> ThumbanailProcessed(Thumbnail thumbnail, byte[] bytes, CancellationToken cancellationToken)
    {
        thumbnail.Bytes = bytes;
        thumbnail.ProcessingStatus = ProcessingStatus.Success;
        thumbnail.EditedAtUtc = DateTime.UtcNow;

        return await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<int> ThumbanailProcessingFailed(Thumbnail thumbnail, CancellationToken cancellationToken)
    {
        thumbnail.ProcessingStatus = ProcessingStatus.Failure;
        thumbnail.EditedAtUtc = DateTime.UtcNow;

        return await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<int> ThumbanilStartsProcessing(Thumbnail thumbnail, CancellationToken cancellationToken)
    {
        thumbnail.ProcessingStatus = ProcessingStatus.Processing;
        thumbnail.EditedAtUtc = DateTime.UtcNow;

        return await dbContext.SaveChangesAsync(cancellationToken);
    }
}
