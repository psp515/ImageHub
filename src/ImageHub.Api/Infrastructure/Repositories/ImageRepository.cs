﻿using ImageHub.Api.Features.Images.Repositories;
using ImageHub.Api.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ImageHub.Api.Infrastructure.Repositories;

public class ImageRepository(ApplicationDbContext dbContext) : IImageRepository
{
    public async Task<int> AddImage(Image image, CancellationToken cancellationToken)
    {
        dbContext.Add(image);
        return await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> UpdateImage(Image image, CancellationToken cancellationToken)
    { 
        dbContext.Update(image);
        return await dbContext.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<bool> DeleteImage(Image image, CancellationToken cancellationToken)
    {
        dbContext.Remove(image);
        return await dbContext.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<bool> ExistsByName(string name, CancellationToken cancellationToken) 
        => await dbContext.Images.AnyAsync(x => x.Name == name, cancellationToken: cancellationToken);

    public async Task<Image?> GetImageById(Guid guid, CancellationToken cancellationToken) => await dbContext.Images.FirstOrDefaultAsync(x => x.Id == guid, cancellationToken);

    public async Task<List<Image>> GetImages(Guid? packId, int page, int size, CancellationToken cancellationToken) 
        => await dbContext.Images
            .Where(x => x.PackId == packId)
            .Skip((page-1)*size)
            .Take(size)
            .ToListAsync(cancellationToken);

    public async Task<Image?> GetImageByImagePackIdAsync(Guid id, CancellationToken cancellationToken) 
        => await dbContext.Images
        .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public async Task<int> GetNumberOfRecords(CancellationToken cancellationToken)
        => await dbContext.Images
        .CountAsync(cancellationToken);

    public async Task<List<Image>> GetImagePacksAsync(Guid packId, int page, int pageSize, CancellationToken cancellationToken) 
        => await dbContext.Images.Where(x => x.PackId == packId)
            .Skip((page-1)*pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

}
