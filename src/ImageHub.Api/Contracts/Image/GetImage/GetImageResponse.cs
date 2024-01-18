namespace ImageHub.Api.Contracts.Image.GetImage;

public class GetImageResponse
{
    public Guid Id { get; set; }
    public Guid PackId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string FileType { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime CreatedAtUtc { get; set; }
    public DateTime UpdatedAtUtc { get; set; }
}
