using ImageHub.Api.Contracts.Image.UpdateImage;

namespace ImageHub.Api.Features.Images.UpdateImage;

public class UpdateImageCommand : IRequest<Result<UpdateImageResponse>>
{
    public string Description { get; set; } = string.Empty;
    public Guid Id { get; set; } = Guid.Empty;
}
