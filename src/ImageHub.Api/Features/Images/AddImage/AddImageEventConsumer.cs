using ImageHub.Api.Features.Images.Repositories;
using ImageHub.Api.Features.Thumbnails;
using MassTransit;
using System.Drawing;

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
        try
        {
            var image = await imageStoreRepository.LoadImage(addEvent.imageKey);

            if (image is null)
            {
                logger.LogError("Event Type: {@RequestName}, Time: {@DateTimeUtc}, ThumbnailId {id}, Image not found on disc.",
                                       typeof(AddImageEvent).Name,
                                       DateTime.UtcNow,
                                       addEvent.ThumbnailId);
                return;
            }


            logger.LogError("Event Type: {@RequestName}, Time: {@DateTimeUtc}, ThumbnailId {id}, Processing succeded.",
                                   typeof(AddImageEvent).Name,
                                   DateTime.UtcNow,
                                   addEvent.ThumbnailId);
        }
        catch (Exception e)
        {
            logger.LogError("Event Type: {@RequestName}, Time: {@DateTimeUtc}, ThumbnailId {id}, Processing failed.",
                                   typeof(AddImageEvent).Name,
                                   DateTime.UtcNow,
                                   addEvent.ThumbnailId);
        }
    }
}
