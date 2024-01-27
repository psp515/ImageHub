using System.Text.Json.Serialization;

namespace ImageHub.Api.Tests.Features.Thumbnails.Models;

public class ThumbnailsDto
{
    [JsonPropertyName("thumbnails")]
    public IEnumerable<ThumbnailDto> Thumbnails { get; set; } = [];
}
