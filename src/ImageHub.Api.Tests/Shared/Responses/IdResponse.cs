using System.Text.Json.Serialization;

namespace ImageHub.Api.Tests.Shared.Responses;

public class IdResponse
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
}
