namespace ImageHub.Api.Features.Thumbnails;

public interface IThumbnailRepository
{
    Task<int> ThumbanailProcessed(Guid id, byte[] bytes);
    Task<int> ThumbnailProcessing(Guid id);
    Task<int> ThumbanailProcessingFailed(Guid id);

    Task<int> AddThumbnailBasedOnImage(Thumbnail thumbnail, CancellationToken cancellationToken);
    Task<List<Thumbnail>> GetThumbnails(Guid? imagePackId,int page, int size, CancellationToken cancellationToken);
    Task<Thumbnail?> GetThumbnail(Guid id, CancellationToken cancellationToken);

    Task<Thumbnail?> GetOldestBlockedThumbnail(TimeSpan buggedFor, CancellationToken cancellationToken);
    Task<Thumbnail?> GetNotStartedProcessingThumbnail(TimeSpan notStartedProcessing, CancellationToken cancellationToken);
}
