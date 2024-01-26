namespace ImageHub.Api.Features.Thumbnails;

public interface IThumbnailRepository
{
    Task<Thumbnail?> CreateThumbnail(Image image);
    Task<bool> ThumbanailProcessed(Guid id, byte[] bytes);
    Task<bool> ThumbanilStartsProcessing(Guid id);
    Task<bool> ThumbanailProcessingFailed(Guid id);

    Task<List<Thumbnail>> GetThumbnails(Guid? imagePackId,int page, int size, CancellationToken cancellationToken);
    Task<Thumbnail?> GetThumbnail(Guid id, CancellationToken cancellationToken);
}
