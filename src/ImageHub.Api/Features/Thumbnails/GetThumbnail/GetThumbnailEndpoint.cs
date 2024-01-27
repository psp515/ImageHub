using ImageHub.Api.Contracts.Thumbnails.GetThumbnail;
using Microsoft.AspNetCore.Mvc;

namespace ImageHub.Api.Features.Thumbnails.GetThumbnail;

public class GetThumbnailEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/thumbnails/{id:guid}", Get)
            .WithTags(ThumbnailExtensions.Name);
    }

    [ProducesResponseType(typeof(GetThumbnailResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IResult> Get(Guid id, ISender sender)
    {
        var query = new GetThumbnailQuery { Id = id };

        var result = await sender.Send(query);

        if (result.IsFailure)
        {
            return result.ToResultsDetails();
        }

        return Results.Ok(result.Value);
    }
}
