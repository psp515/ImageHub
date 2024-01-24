namespace ImageHub.Api.Features.ImagePacks;

public interface IImagePackRepository
{
    Task<bool> ExistsByName(string name, CancellationToken cancellationToken);
    Task<bool> AddImagePack(ImagePack imagePack, CancellationToken cancellationToken);
    Task<bool> UpdateImagePack(ImagePack imagePack, CancellationToken cancellationToken);
    Task<ImagePack?> GetImagePackById(Guid id, CancellationToken cancellationToken);
    Task<List<ImagePack>> GetImagePacks(int page, int size, CancellationToken cancellationToken);
    Task<bool> DeleteImagePack(ImagePack imagePack, CancellationToken cancellationToken);
}