using System.Text.Json.Serialization;

namespace ImageHub.Api.Tests.Features.Image.Models;

public class ImageDto
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;
    [JsonPropertyName("packId")]
    public string? PackId { get; set; } = string.Empty;
    [JsonPropertyName("fileType")]
    public string FileType { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    [JsonPropertyName("createdOnUtc")]
    public DateTime CreatedOnUtc { get; set; } = DateTime.UtcNow;
    [JsonPropertyName("editedAtUtc")]
    public DateTime EditedAtUtc { get; set; } = DateTime.UtcNow;
}
