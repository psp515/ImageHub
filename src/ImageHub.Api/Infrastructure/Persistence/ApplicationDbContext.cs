using Microsoft.EntityFrameworkCore;

namespace ImageHub.Api.Infrastructure.Persistence;

public class ApplicationDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<ImagePack> ImagePacks { get; set; }
    public DbSet<Image> Images { get; set; }
    public DbSet<Thumbnail> Thumbnails { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ImagePack>()
            .ToTable("ImagePacks");
        modelBuilder.Entity<Image>()
            .ToTable("Images");
        modelBuilder.Entity<Thumbnail>()
            .ToTable("Thumbnails");
    }
}
