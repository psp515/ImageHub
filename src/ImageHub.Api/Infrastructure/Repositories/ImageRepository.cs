using ImageHub.Api.Features.Images.Repositories;
using ImageHub.Api.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ImageHub.Api.Infrastructure.Repositories;

public class ImageRepository(ApplicationDbContext dbContext) : IImageRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<bool> AddImage(Image image, CancellationToken cancellationToken)
    {
        _dbContext.Add(image);
        return await _dbContext.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<bool> UpdateImage(Image image, CancellationToken cancellationToken)
    { 
        _dbContext.Update(image);
        return await _dbContext.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<bool> DeleteImage(Image image, CancellationToken cancellationToken)
    {
        _dbContext.Remove(image);
        return await _dbContext.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<bool> ExistsByName(string name, CancellationToken cancellationToken)
    {
        return await _dbContext.Images
            .AnyAsync(x => x.Name == name, cancellationToken: cancellationToken);
    }

    public async Task<Image?> GetImageByImagePackIdAsync(Guid id, CancellationToken cancellationToken) 
        => await _dbContext.Images
        .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public async Task<int> GetNumberOfRecords(CancellationToken cancellationToken)
        => await _dbContext.Images
        .CountAsync(cancellationToken);

    public async Task<List<Image>> GetImagePacksAsync(Guid packId, int page, int pageSize, CancellationToken cancellationToken) 
        => await _dbContext.Images.Where(x => x.PackId == packId)
            .Skip((page-1)*pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
}
