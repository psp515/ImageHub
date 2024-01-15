using System.ComponentModel.DataAnnotations;

namespace ImageHub.Api.Entities;

public class Image
{
    [Key]
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;
    public byte[] Bytes { get; set; } = Array.Empty<byte>();
    public string Description { get; set; } = string.Empty;
    public string FileExtension { get; set; } = string.Empty;
    public decimal Size { get; set; }

    public ImagePack? Group { get; set; } 

    public DateTime CreatedOnUtc { get; set; } = DateTime.UtcNow;
    public DateTime EditedAtUtc { get; set; } = DateTime.UtcNow;
}
