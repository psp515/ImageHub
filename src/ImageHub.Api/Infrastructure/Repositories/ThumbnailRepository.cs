using ImageHub.Api.Features.Thumbnails;
using ImageHub.Api.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ImageHub.Api.Infrastructure.Repositories;

public class ThumbnailRepository(ApplicationDbContext dbContext) : IThumbnailRepository
{
    public async Task<int> AddThumbnailBasedOnImage(Thumbnail thumbnail, CancellationToken cancellationToken)
    {
        dbContext.Add(thumbnail);
        return await dbContext.SaveChangesAsync(cancellationToken);
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

    public async Task<int> ThumbanailProcessed(Guid id, byte[] bytes)
    {
        var thumbnail = await GetThumbnail(id, default);

        if (thumbnail is null)
            return 0;
        
        thumbnail.Bytes = bytes;
        thumbnail.ProcessingStatus = ProcessingStatus.Success;
        thumbnail.EditedAtUtc = DateTime.UtcNow;

        return await dbContext.SaveChangesAsync();
    }

    public async Task<int> ThumbanailProcessingFailed(Guid id)
    {
        var thumbnail = await GetThumbnail(id, default);

        if (thumbnail is null)
            return 0;
        
        thumbnail.ProcessingStatus = ProcessingStatus.Failure;
        thumbnail.EditedAtUtc = DateTime.UtcNow;

        return await dbContext.SaveChangesAsync();
    }

    public async Task<int> ThumbnailProcessing(Guid id)
    {
        var thumbnail = await GetThumbnail(id, default);

        if (thumbnail is null)
            return 0;
        
        thumbnail.ProcessingStatus = ProcessingStatus.Processing;
        thumbnail.EditedAtUtc = DateTime.UtcNow;

        return await dbContext.SaveChangesAsync();
    }
}
