using ImageHub.Api.Shared;
using System.Text.Json.Serialization;

namespace ImageHub.Api.Tests.Shared.Models;

public class Error
{
    [JsonPropertyName("code")]
    public string Code { get; set; } = string.Empty;
    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;
    [JsonPropertyName("type")]
    public ErrorType Type { get; set; } = ErrorType.None;
}
