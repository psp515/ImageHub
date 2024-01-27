namespace ImageHub.Api.Features.Thumbnails;

public interface IThumbnailRepository
{
    Task<int> ThumbanailProcessed(Thumbnail thumbnail, byte[] bytes, CancellationToken cancellationToken);
    Task<int> ThumbanilStartsProcessing(Thumbnail thumbnail, CancellationToken cancellationToken);
    Task<int> ThumbanailProcessingFailed(Thumbnail thumbnail, CancellationToken cancellationToken);

    Task<Thumbnail?> AddThumbnailBasedOnImage(Image image, CancellationToken cancellationToken);
    Task<List<Thumbnail>> GetThumbnails(Guid? imagePackId,int page, int size, CancellationToken cancellationToken);
    Task<Thumbnail?> GetThumbnail(Guid id, CancellationToken cancellationToken);
}
