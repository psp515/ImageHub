using ImageHub.Api.Extensions;

namespace ImageHub.Api.Features.ImagePacks.DeleteImagePack;

public class DeleteImagePackEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/api/imagepacks/{id:guid}", async (Guid id, ISender sender) =>
        {
            var command = new DeleteImagePackCommand { Id = id };

            var result = await sender.Send(command);

            return result.IsSuccess ? Results.NoContent() : result.ToResultsDetails();
        }).WithTags(ImagePacksExtensions.Name);
    }
}
