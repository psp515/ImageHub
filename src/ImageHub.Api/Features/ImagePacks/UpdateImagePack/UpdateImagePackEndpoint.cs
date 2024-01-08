using ImageHub.Api.Contracts.ImagePacks;

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

            if (result.IsFailure)
            {
                return Results.BadRequest(result.Error);
            }

            return Results.NoContent();
        });
    }
}
