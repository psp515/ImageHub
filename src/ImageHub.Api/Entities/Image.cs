using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ImageHub.Api.Entities;

public class Image
{
    [Key]
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;
    public string ImageStoreKey { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string FileType { get; set; } = string.Empty;

    public Guid? PackId { get; set; }
    [ForeignKey("PackId")]
    public ImagePack? Pack { get; set; } 

    public DateTime CreatedOnUtc { get; set; } = DateTime.UtcNow;
    public DateTime EditedAtUtc { get; set; } = DateTime.UtcNow;
}
