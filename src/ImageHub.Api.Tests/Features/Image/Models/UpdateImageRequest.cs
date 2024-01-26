using System.Text.Json.Serialization;

namespace ImageHub.Api.Tests.Features.Image.Models;

public class UpdateImageRequest
{
    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;
}
