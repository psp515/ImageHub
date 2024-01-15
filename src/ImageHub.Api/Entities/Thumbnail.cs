using System.ComponentModel.DataAnnotations;

namespace ImageHub.Api.Entities;

public class Thumbnail
{
    [Key]
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;
    public byte[] Bytes { get; set; } = [];
    public string Description { get; set; } = string.Empty;
    public string FileExtension { get; set; } = string.Empty;
    public decimal Size { get; set; }

    public Image Image { get; set; } = default!;

    public DateTime CreatedOnUtc { get; set; } = DateTime.UtcNow;
    public DateTime EditedAtUtc { get; set; } = DateTime.UtcNow;
}
