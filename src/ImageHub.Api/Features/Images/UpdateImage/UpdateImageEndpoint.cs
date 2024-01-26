using ImageHub.Api.Contracts.Image.UpdateImage;
using Microsoft.AspNetCore.Mvc;

namespace ImageHub.Api.Features.Images.UpdateImage;

public class UpdateImageEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPatch("/api/images/{id:guid}", Update).WithTags(ImagesExtensions.Name);
    }

    [ProducesResponseType(typeof(UpdateImageResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IResult> Update(Guid id,
            UpdateImageRequest request,
            ISender sender)
    {
        var command = new UpdateImageCommand
        {
            Id = id,
            Description = request.Description
        };

        var result = await sender.Send(command);

        if (result.IsFailure)
            return result.ToResultsDetails();

        var response = result.Value;

        return Results.Ok(response);
    }
}
