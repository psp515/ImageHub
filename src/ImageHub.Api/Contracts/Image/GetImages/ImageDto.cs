namespace ImageHub.Api.Contracts.Image.GetImages;

public class ImageDto
{
    public Guid Id { get; set; }
    public Guid? PackId { get; set; } = default;
    public string Name { get; set; } = string.Empty;
    public string FileType { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime CreatedOnUtc { get; set; }
    public DateTime EditedAtUtc { get; set; }
}
