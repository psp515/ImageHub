using System.Text.Json.Serialization;

namespace ImageHub.Api.Tests.Shared.Responses;

public class AntiforgeryTokenResponse
{
    [JsonPropertyName("token")]
    public string Token { get; set; } = string.Empty;
}
