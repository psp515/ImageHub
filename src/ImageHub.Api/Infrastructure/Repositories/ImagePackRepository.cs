using ImageHub.Api.Features.ImagePacks;
using ImageHub.Api.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ImageHub.Api.Infrastructure.Repositories;

public class ImagePackRepository(ApplicationDbContext dbContext) : IImagePackRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<bool> AddImagePack(ImagePack imagePack, CancellationToken cancellationToken)
    {
        _dbContext.Add(imagePack);
        return await _dbContext.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<bool> UpdateImagePack(ImagePack imagePack, CancellationToken cancellationToken)
    { 
        _dbContext.Update(imagePack);
        return await _dbContext.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<bool> ExistsByName(string name, CancellationToken cancellationToken) 
        => await _dbContext.ImagePacks
            .AnyAsync(x => x.Name == name, cancellationToken);

    public async Task<ImagePack?> GetImagePackById(Guid id, CancellationToken cancellationToken) 
        => await _dbContext.ImagePacks
        .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public async Task<List<ImagePack>> GetImagePacks(int page, int size, CancellationToken cancellationToken)
        => await _dbContext.ImagePacks
        .Skip((page-1) * size)
        .Take(size)
        .ToListAsync(cancellationToken);

    public async Task<bool> DeleteImagePack(ImagePack imagePack, CancellationToken cancellationToken)
    {
        _dbContext.Remove(imagePack);
        return await _dbContext.SaveChangesAsync(cancellationToken) > 0;
    }
}
