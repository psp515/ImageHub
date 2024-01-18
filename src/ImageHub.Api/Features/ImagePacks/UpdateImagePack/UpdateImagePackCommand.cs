
namespace ImageHub.Api.Features.ImagePacks.AddImagePack;

public class UpdateImagePackCommand : IRequest<Result>
{
    public string Description { get; set; } = string.Empty;
    public string Id { get; set; } = string.Empty;
}
