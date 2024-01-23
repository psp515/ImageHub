using ImageHub.Api.Contracts.Image.UpdateImage;
using ImageHub.Api.Extensions;

namespace ImageHub.Api.Features.Images.UpdateImage;

public class UpdateImageEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPatch("/api/images/{id:guid}", async (Guid id, 
            UpdateImageRequest request, 
            ISender sender) => 
        {
            var command = new UpdateImageCommand
            {
                Id = id,
                Description = request.Description
            };

            var result = await sender.Send(command);

            if (result.IsFailure)
            {
                return result.ToResultsDetails();
            }

            return Results.Ok(result.Value);
        }).WithTags(ImagesExtensions.Name);
    }
}
