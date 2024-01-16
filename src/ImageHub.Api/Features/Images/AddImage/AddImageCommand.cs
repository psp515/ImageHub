using ImageHub.Api.Contracts.Image.AddImage;
using ImageHub.Api.Features.ImagePacks;

namespace ImageHub.Api.Features.Images.AddImage;

public class AddImageCommand : IRequest<Result<AddImageResponse>>
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public FileTypes FileExtension { get; set; }

    public Guid? GroupId { get; set; }

    public required IFormFile Image { get; set; }
}
