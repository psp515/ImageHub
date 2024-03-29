﻿using ImageHub.Api.Entities;
using System.Text.Json.Serialization;

namespace ImageHub.Api.Tests.Features.Thumbnails.Models;

public class ThumbnailDto
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
    [JsonPropertyName("imageId")]
    public Guid ImageId { get; set; }

    [JsonPropertyName("encoding")]
    public string Encoding { get; set; } = string.Empty;
    [JsonPropertyName("encodedImage")] 
    public string EncodedImage { get; set; } = string.Empty;
    [JsonPropertyName("fileExtension")]
    public string FileExtension { get; set; } = string.Empty;
    [JsonPropertyName("processingStatus")]
    public ProcessingStatus ProcessingStatus { get; set; } = ProcessingStatus.NotStarted;

    [JsonPropertyName("createdOnUtc")]
    public DateTime CreatedOnUtc { get; set; } = DateTime.UtcNow;
    [JsonPropertyName("editedAtUtc")]
    public DateTime EditedAtUtc { get; set; } = DateTime.UtcNow;
}
