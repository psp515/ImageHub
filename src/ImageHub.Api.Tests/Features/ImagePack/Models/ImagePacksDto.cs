using System.Text.Json.Serialization;

namespace ImageHub.Api.Tests.Features.ImagePack.Models;

public class ImagePacksDto
{
    [JsonPropertyName("items")]
    public List<ImagePackDto> Items { get; set; } = [];
}
