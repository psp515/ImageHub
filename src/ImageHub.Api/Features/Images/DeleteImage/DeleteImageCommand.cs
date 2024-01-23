using ImageHub.Api.Contracts.Image.DeleteImage;

namespace ImageHub.Api.Features.Images.DeteleImage;

public class DeleteImageCommand : IRequest<Result<DeleteImageResponse>>
{
    public Guid Id { get; set; } = Guid.Empty;
}

