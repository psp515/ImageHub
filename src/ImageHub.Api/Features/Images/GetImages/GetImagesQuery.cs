namespace ImageHub.Api.Features.Images.GetImagesByPack;

public class GetImagesQuery : IRequest<Result<List<Image>>>
{
    public Guid PackId { get; set; } = Guid.Empty;
    public int Size { get; set; }
    public int Page { get; set; }
}
