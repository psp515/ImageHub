namespace ImageHub.Api.Features.ImagePacks.GetImagePack;

public class GetImagePackQuery : IRequest<Result<GetImagePackResponse>>
{
    public Guid Id { get; set; }
}
