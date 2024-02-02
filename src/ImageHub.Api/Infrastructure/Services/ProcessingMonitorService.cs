using ImageHub.Api.Features.Images.AddImage;
using ImageHub.Api.Features.Images.Repositories;
using ImageHub.Api.Features.Thumbnails;
using System.Threading;

namespace ImageHub.Api.Infrastructure.Services;

public class ProcessingMonitorService(ILogger<ProcessingMonitorService> logger, IServiceProvider serviceProvider) : BackgroundService
{
    private IThumbnailRepository thumbnailRepository;
    private IImageRepository imageRepository;
    private IEventBus eventBus;

    public Task StartAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Time: {@DateTimeUtc}, Background processing monitor starts.",
                DateTime.UtcNow);

        return Task.CompletedTask;
    }

    private async Task ReprocessInvalidAsync(Thumbnail thumbnail, CancellationToken cancellationToken)
    {
        var image = await imageRepository.GetImageById(thumbnail.ImageId, cancellationToken);

        if (image is null)
            return;

        await eventBus.Publish(new AddImageEvent(thumbnail.Id, image.ImageStoreKey), cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Time: {@DateTimeUtc}, Background processing monitor stops.",
                DateTime.UtcNow);

        return Task.CompletedTask;
    }

    protected async override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var scope = serviceProvider.CreateScope();

        thumbnailRepository = scope.ServiceProvider.GetRequiredService<IThumbnailRepository>();
        imageRepository = scope.ServiceProvider.GetRequiredService<IImageRepository>();
        eventBus = scope.ServiceProvider.GetRequiredService<IEventBus>();

        var buggedFor = TimeSpan.FromMinutes(1);
        var notStartedProcessing = TimeSpan.FromMinutes(1);
        var loopDelay = 5000;

        while (!stoppingToken.IsCancellationRequested)
        {
            var blockedThumbnail = await thumbnailRepository.GetOldestBlockedThumbnail(buggedFor, stoppingToken);

            if (blockedThumbnail is not null)
            {
                logger.LogInformation("Time: {@DateTimeUtc}, Thumbnail id: {@id} Thumbnail was blocked for more than {@time} - recreating processing event.",
                    DateTime.UtcNow,
                    blockedThumbnail.Id,
                buggedFor);

                await ReprocessInvalidAsync(blockedThumbnail, stoppingToken);
            }

            var processingNotStarted = await thumbnailRepository.GetNotStartedProcessingThumbnail(notStartedProcessing, stoppingToken);

            if (processingNotStarted is not null)
            {
                logger.LogInformation("Time: {@DateTimeUtc}, Thumbnail id: {@id} Thumbnail was not processed for longer than {@time} - recreating processing event.",
                    DateTime.UtcNow,
                    processingNotStarted.Id,
                    notStartedProcessing);

                await ReprocessInvalidAsync(processingNotStarted, stoppingToken);
            }

            await Task.Delay(loopDelay, stoppingToken);
        }
    }
}
