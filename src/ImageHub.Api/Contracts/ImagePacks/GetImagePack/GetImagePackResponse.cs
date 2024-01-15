namespace ImageHub.Api.Contracts.ImagePacks.GetImagePack;

public class GetImagePackResponse
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public DateTime CreatedOnUtc { get; set; } = DateTime.UtcNow;
    public DateTime EditedAtUtc { get; set; } = DateTime.UtcNow;
}
