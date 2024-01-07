using ImageHub.Api.Contracts.ImagePacks;
using Mapster;

namespace ImageHub.Api.Features.ImagePacks.AddImagePack;

public class EditImagePackEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/imagepack/{id}", async (Guid id, EditImagePackRequest request, ISender sender) =>
        {
            var command = new EditImagePackCommand
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
