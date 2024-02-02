using Microsoft.AspNetCore.SignalR;

namespace ImageHub.Api.Features.Thumbnails.Hubs;

public class ThumbnailHub : Hub<IThumbnailHub>
{
    public override async Task OnConnectedAsync()
    {
        await Clients.Caller
            .ConnectionInitialized($"Hello {Context.User?.Identity?.Name}. Now you will receive notifications when any thumbnail is processed.");
    }
}
