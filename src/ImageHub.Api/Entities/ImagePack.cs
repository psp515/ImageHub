using System.ComponentModel.DataAnnotations;

namespace ImageHub.Api.Entities;

public class ImagePack
{
    [Key]
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;


    public IList<Image> Images { get; set; } = new List<Image>();

    public DateTime CreatedOnUtc { get; set; } = DateTime.UtcNow;
    public DateTime EditedAtUtc { get; set; } = DateTime.UtcNow;
}
