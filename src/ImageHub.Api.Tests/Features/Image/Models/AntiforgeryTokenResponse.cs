using System.Text.Json.Serialization;

namespace ImageHub.Api.Tests.Features.Image.Models;

public class AntiforgeryTokenResponse
{
    [JsonPropertyName("token")]
    public string Token { get; set; } = string.Empty;
}
