using System.Text.Json.Serialization;

namespace ImageHub.Api.Tests.Features.Image.Models;

public class ImagesDto
{
    [JsonPropertyName("images")]
    public List<ImageDto> Images { get; set; } = [];
}
