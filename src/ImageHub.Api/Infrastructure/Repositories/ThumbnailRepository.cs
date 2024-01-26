using ImageHub.Api.Features.Thumbnails;
using ImageHub.Api.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ImageHub.Api.Infrastructure.Repositories;

public class ThumbnailRepository(ApplicationDbContext dbContext) : IThumbnailRepository
{
    public Task<Thumbnail?> CreateThumbnail(Image image)
    {
        throw new NotImplementedException();
    }

    public async Task<Thumbnail?> GetThumbnail(Guid id, CancellationToken cancellationToken)
    {
        return await dbContext.Thumbnails
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<List<Thumbnail>> GetThumbnails(Guid? imagePackId, int page, int size, CancellationToken cancellationToken)
        => await dbContext.Thumbnails
            .Where(x => x.Image.PackId == imagePackId)
            .Skip((page-1)*size)
            .Take(size)
            .ToListAsync(cancellationToken);

    public Task<bool> ThumbanailProcessed(Guid id, byte[] bytes)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ThumbanailProcessingFailed(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ThumbanilStartsProcessing(Guid id)
    {
        throw new NotImplementedException();
    }
}
