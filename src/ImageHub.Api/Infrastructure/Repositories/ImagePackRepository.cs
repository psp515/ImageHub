using ImageHub.Api.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ImageHub.Api.Infrastructure.Repositories;

public class ImagePackRepository : IImagePackRepository
{
    private readonly ApplicationDbContext _dbContext;

    public ImagePackRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

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
}
