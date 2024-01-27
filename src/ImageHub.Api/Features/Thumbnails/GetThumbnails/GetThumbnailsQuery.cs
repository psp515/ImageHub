namespace ImageHub.Api.Features.Thumbnails.GetThumbnails;

public class GetThumbnailsQuery : IRequest<Result<List<Thumbnail>>>
{
    public Guid? PackId { get; set; } = default;
    public int Size { get; set; }
    public int Page { get; set; }
}
