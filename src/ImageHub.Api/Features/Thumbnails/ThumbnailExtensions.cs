using ImageHub.Api.Infrastructure.Repositories;

namespace ImageHub.Api.Features.Thumbnails;

public static class ThumbnailExtensions
{
    public static readonly string BaseEncoding = "base64";
    public static readonly string ThumbnailExtension = "image/png";
    public static readonly int BoundingBox = 120;
    public static string Name => "Thumbnails Controller";

    public static WebApplicationBuilder RegisterThumbnailServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IThumbnailRepository, ThumbnailRepository>();
        return builder;
    }
}
