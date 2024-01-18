using ImageHub.Api.Contracts.ImagePacks.GetImagePack;

namespace ImageHub.Api.Features.ImagePacks.GetImagePack;

public class GetImagePackQuery : IRequest<Result<GetImagePackResponse>>
{
    public string Id { get; set; }
}
