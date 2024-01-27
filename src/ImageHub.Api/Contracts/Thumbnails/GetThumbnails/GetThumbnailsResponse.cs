namespace ImageHub.Api.Contracts.Thumbnails.GetThumbnails;

public class GetThumbnailsResponse
{
    public IEnumerable<ThumbnailDto> Thumbnails { get; set; } = [];
}
