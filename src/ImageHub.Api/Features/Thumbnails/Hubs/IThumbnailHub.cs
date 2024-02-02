namespace ImageHub.Api.Features.Thumbnails.Hubs;

public interface IThumbnailHub
{
    Task ThumbnailProcessed(string thumbnailId, ProcessingStatus processingStatus);
    Task ConnectionInitialized(string message);
}
