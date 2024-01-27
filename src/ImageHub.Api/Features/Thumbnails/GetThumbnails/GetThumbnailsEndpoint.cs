using ImageHub.Api.Contracts.Thumbnails.GetThumbnails;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace ImageHub.Api.Features.Thumbnails.GetThumbnails;

public class GetThumbnailsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/thumbnails", Get)
            .WithTags(ThumbnailExtensions.Name);
    }

    [ProducesResponseType(typeof(GetThumbnailsResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IResult> Get(ISender sender,
            Guid? packId = null,
            int page = 1, int
            size = 25)
    {
        var query = new GetThumbnailsQuery
        {
            PackId = packId,
            Page = page,
            Size = size
        };

        var result = await sender.Send(query);

        if (result.IsFailure)
            return result.ToResultsDetails();

        var dtos = result.Value
            .Select(x => x.Adapt<ThumbnailDto>())
            .ToList();

        var response = new GetThumbnailsResponse
        {
            Thumbnails = dtos
        };

        return Results.Ok(response);
    }
}
