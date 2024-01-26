using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ImageHub.Api.Entities;

public class Thumbnail
{
    [Key]
    public Guid Id { get; set; }

    public byte[] Bytes { get; set; } = [];
    public string FileExtension { get; set; } = string.Empty;
    public ProcessingStatus ProcessingStatus { get; set; } = ProcessingStatus.NotStarted;

    public Guid ImageId { get; set; }
    [ForeignKey("ImageId")]
    public Image Image { get; set; } = default!;

    public DateTime CreatedOnUtc { get; set; } = DateTime.UtcNow;
    public DateTime EditedAtUtc { get; set; } = DateTime.UtcNow;
}
