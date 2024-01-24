using ImageHub.Api.Contracts.Image.GetImageFile;

namespace ImageHub.Api.Features.Images.GetImageFile;

public class GetImageFileQuery : IRequest<Result<GetImageFileResponse>>
{
    public Guid Id { get; set; } = Guid.Empty;
}
