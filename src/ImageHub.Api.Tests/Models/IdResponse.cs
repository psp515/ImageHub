using System.Text.Json.Serialization;

namespace ImageHub.Api.Tests.Models;

public class IdResponse
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
}
