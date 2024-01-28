using ImageHub.Api.Features.Images.Repositories;
using ImageHub.Api.Features.Thumbnails;
using MassTransit;
using SkiaSharp;

namespace ImageHub.Api.Features.Images.AddImage;

public sealed class AddImageEventConsumer(
    IImageStoreRepository imageStoreRepository,
    IThumbnailRepository thumbnailRepository,
    ILogger<AddImageEventConsumer> logger)
    : IConsumer<AddImageEvent>
{
    public async Task Consume(ConsumeContext<AddImageEvent> context)
    {
        var addEvent = context.Message;

        logger.LogInformation("Event Type: {@RequestName}, Time: {@DateTimeUtc}, ThumbnailId {id}, Event received from event bus.",
                   typeof(AddImageEvent).Name,
                   DateTime.UtcNow,
                   addEvent.ThumbnailId);

        await thumbnailRepository
            .ThumbnailProcessing(addEvent.ThumbnailId);

        try
        {
            var image = await imageStoreRepository
                .LoadImage(addEvent.imageKey);

            if (image is null)
            {
                logger.LogError("Event Type: {@RequestName}, Time: {@DateTimeUtc}, ThumbnailId {id}, Image not found on disc.",
                                       typeof(AddImageEvent).Name,
                                       DateTime.UtcNow,
                                       addEvent.ThumbnailId);
                return;
            }

            using var imageStream = new MemoryStream(image);
            var thumbnailBytes = CreateThumbnail(image);

            await thumbnailRepository
                .ThumbanailProcessed(addEvent.ThumbnailId, thumbnailBytes);

            logger.LogInformation("Event Type: {@RequestName}, Time: {@DateTimeUtc}, ThumbnailId {id}, Processing succeded.",
                                   typeof(AddImageEvent).Name,
                                   DateTime.UtcNow,
                                   addEvent.ThumbnailId);
        }
        catch (Exception e)
        {
            logger.LogError("Event Type: {@RequestName}, Time: {@DateTimeUtc}, ThumbnailId {id}, Processing failed. {exception}",
                                   typeof(AddImageEvent).Name,
                                   DateTime.UtcNow,
                                   addEvent.ThumbnailId,
                                   e.Message);

            await thumbnailRepository
                .ThumbanailProcessingFailed(addEvent.ThumbnailId);
        }
    }

    private byte[] CreateThumbnail(byte[] image)
    {
        using SKStream skStream = new SKMemoryStream(image);
        using SKBitmap bitmap = SKBitmap.Decode(skStream);

        var box = ThumbnailExtensions.BoundingBox;

        int width = bitmap.Width > bitmap.Height ?
            box : ThumbanilScaler(bitmap.Height, bitmap.Width, box);
        int height = bitmap.Height > bitmap.Height ?
            ThumbanilScaler(bitmap.Width, bitmap.Height, box) : box;

        using SKBitmap scaledBitmap = bitmap.Resize(new SKImageInfo(width, height), SKFilterQuality.High);
        using SKImage scaledImage = SKImage.FromBitmap(scaledBitmap);
        using SKData data = scaledImage.Encode();

        return data.ToArray();
    }

    public int ThumbanilScaler(int a, int b, int box)
    {
        float size = a * b / box;
        return (int)size;
    }
}
