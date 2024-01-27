using ImageHub.Api.Contracts.Thumbnails.GetThumbnail;

namespace ImageHub.Api.Features.Thumbnails.GetThumbnail;

public class GetThumbnailQuery : IRequest<Result<GetThumbnailResponse>>
{
    public Guid Id { get; set; }
}
