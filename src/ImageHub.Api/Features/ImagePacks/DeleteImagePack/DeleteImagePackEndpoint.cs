using ImageHub.Api.Contracts.ImagePacks.DeleteImagePack;
using Microsoft.AspNetCore.Mvc;

namespace ImageHub.Api.Features.ImagePacks.DeleteImagePack;

public class DeleteImagePackEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/api/imagepacks/{id:guid}", Delete)
            .WithTags(ImagePacksExtensions.Name);
    }

    [ProducesResponseType(typeof(DeleteImagePackResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IResult> Delete(Guid id, ISender sender)
    {
        var command = new DeleteImagePackCommand { Id = id };

        var result = await sender.Send(command);

        return result.IsSuccess ? Results.NoContent() : result.ToResultsDetails();
    }
}
