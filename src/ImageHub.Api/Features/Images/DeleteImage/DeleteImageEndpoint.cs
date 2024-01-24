using ImageHub.Api.Contracts.Image.AddImage;
using ImageHub.Api.Contracts.Image.DeleteImage;
using ImageHub.Api.Extensions;
using ImageHub.Api.Features.Images.DeteleImage;
using Microsoft.AspNetCore.Mvc;

namespace ImageHub.Api.Features.Images.DeleteImage;

public class DeleteImageEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/api/images/{id:guid}", async (Guid id, ISender sender) =>
        {
            var query = new DeleteImageCommand { Id = id };

            var result = await sender.Send(query);

            return result.IsSuccess ? Results.Ok(result) : result.ToResultsDetails();

        }).WithTags(ImagesExtensions.Name);
    }

    [ProducesResponseType(typeof(DeleteImageResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IResult> Delete(Guid id, ISender sender)
    {
        var query = new DeleteImageCommand { Id = id };

        var result = await sender.Send(query);

        return result.IsSuccess ? Results.Ok(result) : result.ToResultsDetails();

    }
}
