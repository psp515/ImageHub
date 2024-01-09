using ImageHub.Api.Contracts.ImagePacks;
using ImageHub.Api.Extensions;
using Mapster;

namespace ImageHub.Api.Features.ImagePacks.AddImagePack;

public class AddImagePackEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/imagepack", async (AddImagePackRequest request, ISender sender) =>
        {
            var command = request.Adapt<AddImagePackCommand>();

            var result = await sender.Send(command);

            if (result.IsFailure)
            {
                return result.ToResultsDetails();
            }

            var response = new AddImagePackResponse(result.Value);

            return Results.Created($"/api/imagepack/{result.Value}", response);
        });
    }
}
