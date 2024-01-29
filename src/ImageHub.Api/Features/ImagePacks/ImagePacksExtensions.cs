using ImageHub.Api.Infrastructure.Repositories;

namespace ImageHub.Api.Features.ImagePacks;

public static class ImagePacksExtensions
{
    public static string Name => "ImagePacks Controller";

    public static WebApplicationBuilder RegisterImagePacksServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IImagePackRepository, ImagePackRepository>();
        return builder;
    }
}
