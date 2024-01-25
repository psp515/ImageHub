using ImageHub.Api.Contracts.ImagePacks.UpdateImagePack;
using Microsoft.AspNetCore.Mvc;

namespace ImageHub.Api.Features.ImagePacks.AddImagePack;

public class UpdateImagePackEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPatch("/api/imagepacks/{id:guid}", Update)
            .WithTags(ImagePacksExtensions.Name);
    }

    [ProducesResponseType(typeof(UpdateImagePackResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IResult> Update(Guid id,
            UpdateImagePackRequest request,
            ISender sender)
    {
        var command = new UpdateImagePackCommand
        {
            Id = id,
            Description = request.Description,
        };

        var result = await sender.Send(command);

        return result.IsSuccess ? Results.Ok(result.Value) : result.ToResultsDetails();
    }
}
