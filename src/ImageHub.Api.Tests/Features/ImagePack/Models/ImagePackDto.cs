using System.Text.Json.Serialization;

namespace ImageHub.Api.Tests.Features.ImagePack.Models;

public class ImagePackDto
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    [JsonPropertyName("createdOnUtc")]
    public DateTime CreatedOnUtc { get; set; } = DateTime.UtcNow;
    [JsonPropertyName("editedAtUtc")]
    public DateTime EditedAtUtc { get; set; } = DateTime.UtcNow;
}
