namespace ImageHub.Api.Contracts.Image.GetImages;

public class GetImagesResponse
{
    public IEnumerable<ImageDto> Images { get; set; } = [];
}
