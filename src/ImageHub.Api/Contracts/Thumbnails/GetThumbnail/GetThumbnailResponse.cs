﻿namespace ImageHub.Api.Contracts.Thumbnails.GetThumbnail;

public class GetThumbnailResponse
{
    public Guid Id { get; set; }
    public Guid ImageId { get; set; }

    public string EncodedImage { get; set; } = string.Empty;
    public string Encoding { get; set; } = string.Empty;
    public string FileExtension { get; set; } = string.Empty;
    public ProcessingStatus ProcessingStatus { get; set; } = ProcessingStatus.NotStarted;

    public DateTime CreatedOnUtc { get; set; } = DateTime.UtcNow;
    public DateTime EditedAtUtc { get; set; } = DateTime.UtcNow;
}
