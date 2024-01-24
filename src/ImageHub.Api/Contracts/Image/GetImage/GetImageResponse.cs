namespace ImageHub.Api.Contracts.Image.GetImage;

public class GetImageResponse
{
    public string Id { get; set; } = string.Empty;
    public string PackId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string FileType { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime CreatedOnUtc { get; set; }
    public DateTime EditedAtUtc { get; set; }
}
