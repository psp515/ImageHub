using ImageHub.Api.Contracts.Image.GetImage;
using Microsoft.AspNetCore.Mvc;

namespace ImageHub.Api.Features.Images.GetImage;

public class GetImageEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/images/{id:guid}", Get)
            .WithTags(ImagesExtensions.Name);
    }

    [ProducesResponseType(typeof(GetImageResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IResult> Get(Guid id, ISender sender)
    {
        var query = new GetImageQuery { Id = id };

        var result = await sender.Send(query);

        return result.IsSuccess ? Results.Ok(result) : result.ToResultsDetails();
    }
}
