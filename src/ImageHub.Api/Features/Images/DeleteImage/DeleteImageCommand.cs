using ImageHub.Api.Contracts.Image.DeleteImage;

namespace ImageHub.Api.Features.Images.DeteleImage;

public class DeleteImageCommand : IRequest<Result<DeleteImageResponse>>
{
    public string Id { get; set; } = string.Empty;
}

