using ImageHub.Api.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ImageHub.Api.Infrastructure.Repositories;

public class ImagePackRepository(ApplicationDbContext dbContext) : IImagePackRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task AddImagePack(ImagePack imagePack, CancellationToken cancellationToken)
    {
        _dbContext.Add(imagePack);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> ExistsByName(string name, CancellationToken cancellationToken)
    {
        return await _dbContext.ImagePacks
            .AnyAsync(x => x.Name == name, cancellationToken);
    }

    public async Task<ImagePack?> GetImagePackByIdAsync(Guid id, CancellationToken cancellationToken) 
        => await _dbContext.ImagePacks.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public async Task<List<ImagePack>> GetImagePacksAsync(CancellationToken cancellationToken)
        => await _dbContext.ImagePacks.ToListAsync(cancellationToken);
}
