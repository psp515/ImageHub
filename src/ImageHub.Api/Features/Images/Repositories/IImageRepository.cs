namespace ImageHub.Api.Features.Images.Repositories;

public interface IImageRepository
{
    Task<bool> AddImage(Image image, CancellationToken cancellationToken);
    Task<bool> UpdateImage(Image imagePack, CancellationToken cancellationToken);
    Task<bool> DeleteImage(Image imagePack, CancellationToken cancellationToken);
    Task<bool> ExistsByName(string name, CancellationToken cancellationToken);
    Task<Image?> GetImageById(Guid guid, CancellationToken cancellationToken);
    Task<List<Image>> GetImages(Guid? packId, int page, int size, CancellationToken cancellationToken);
}
