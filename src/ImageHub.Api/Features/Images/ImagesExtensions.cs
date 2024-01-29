using ImageHub.Api.Features.Images.Repositories;
using ImageHub.Api.Infrastructure.Repositories;

namespace ImageHub.Api.Features.Images;

public static class ImagesExtensions
{
    public static string Name => "Images Controller";

    public static WebApplicationBuilder RegisterImagesServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IImageRepository, ImageRepository>();
        builder.Services.AddScoped<IImageStoreRepository, FileRepository>();
        return builder;
    }
}
