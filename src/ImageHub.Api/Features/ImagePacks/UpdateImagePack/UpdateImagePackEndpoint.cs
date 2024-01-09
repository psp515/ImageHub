using ImageHub.Api.Contracts.ImagePacks;
using ImageHub.Api.Extensions;

namespace ImageHub.Api.Features.ImagePacks.AddImagePack;

public class UpdateImagePackEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPatch("/api/imagepack/{id}", async (Guid id, UpdateImagePackRequest request, ISender sender) =>
        {
            var command = new UpdateImagePackCommand
            {
                Id = id,
                Description = request.Description,
            };

            var result = await sender.Send(command);

            return result.IsSuccess ? Results.NoContent() : result.ToResultsDetails();
        });
    }
}
