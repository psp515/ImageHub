using ImageHub.Api.Contracts.ImagePacks;

namespace ImageHub.Api.Features.ImagePacks.AddImagePack;

public class AddImagePackEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/imagepack", async (AddImagePackCommand command, ISender sender) =>
        {
            var result = await sender.Send(command);

            if (result.IsFailure)
            {
                return Results.BadRequest(result.Error);
            }

            var response = new AddImagePackResponse(result.Value);

            return Results.Created($"/api/imagepack/{result.Value}", response);
        });
    }
}

