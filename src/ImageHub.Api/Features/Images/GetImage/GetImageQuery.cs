using ImageHub.Api.Contracts.Image.GetImage;

namespace ImageHub.Api.Features.Images.GetImage;

public class GetImageQuery : IRequest<Result<GetImageResponse>>
{
    public Guid Id { get; set; } = Guid.Empty;
}
