namespace ImageHub.Api.Contracts.Image.AddImage;

public class AddImageRequest
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public IFormFile Image { get; set; } = default!;
}
