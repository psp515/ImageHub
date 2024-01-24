using ImageHub.Api.Tests.Shared.Models;
using System.Net;
using System.Text.Json.Serialization;

namespace ImageHub.Api.Tests.Shared.Responses;

public class ErrorResponse
{
    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;
    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;
    [JsonPropertyName("status")]
    public HttpStatusCode Status { get; set; }

    [JsonPropertyName("errors")]
    public IList<Error> Errors { get; set; } = [];
}
