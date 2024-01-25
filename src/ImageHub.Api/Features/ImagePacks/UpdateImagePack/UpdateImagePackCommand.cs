
using ImageHub.Api.Contracts.ImagePacks.UpdateImagePack;

namespace ImageHub.Api.Features.ImagePacks.AddImagePack;

public class UpdateImagePackCommand : IRequest<Result<UpdateImagePackResponse>>
{
    public string Description { get; set; } = string.Empty;
    public Guid Id { get; set; } = Guid.Empty;
}
