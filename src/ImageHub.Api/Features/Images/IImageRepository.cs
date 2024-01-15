namespace ImageHub.Api.Features.Images;

public interface IImageRepository
{
    Task<bool> AddImage(Image image, CancellationToken cancellationToken);
    Task<bool> UpdateImage(Image imagePack, CancellationToken cancellationToken);
    Task<bool> DeleteImage(Image imagePack, CancellationToken cancellationToken);

    Task<Image?> GetImageByImagePackIdAsync(Guid id, CancellationToken cancellationToken);
    Task<int> GetNumberOfRecords(CancellationToken cancellationToken);
    Task<List<Image>> GetImagePacksAsync(Guid packId, int page, int pageSize, CancellationToken cancellationToken);
}
