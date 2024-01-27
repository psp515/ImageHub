namespace ImageHub.Api.Contracts.Thumbnails.GetThumbnails;

public class ThumbnailDto
{
    public Guid Id { get; set; }
    public Guid ImageId { get; set; }

    public byte[] Bytes { get; set; } = [];
    public string FileExtension { get; set; } = string.Empty;
    public ProcessingStatus ProcessingStatus { get; set; } = ProcessingStatus.NotStarted;

    public DateTime CreatedOnUtc { get; set; } = DateTime.UtcNow;
    public DateTime EditedAtUtc { get; set; } = DateTime.UtcNow;
}
