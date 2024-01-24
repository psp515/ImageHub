using ImageHub.Api.Contracts.ImagePacks.GetImagePacks;

namespace ImageHub.Api.Features.ImagePacks.GetImagePack;

public class GetImagePacksQuery : IRequest<Result<GetImagePacksResponse>>
{
    public int Size { get; set; }
    public int Page { get; set; }
}