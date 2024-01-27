
using System.Text.Json.Serialization;

namespace ImageHub.Api.Tests.Shared.Responses;

public class AddImageResponse
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
    [JsonPropertyName("thumbnailId")]
    public Guid ThumbnailId { get; set; }
}
