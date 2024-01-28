using ImageHub.Api.Contracts.Thumbnails.GetThumbnails;
using ImageHub.Api.Entities;
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
            .Select(x => new ThumbnailDto
            {
                Id = x.Id,
                Encoding = ThumbnailExtensions.BaseEncoding,
                EncodedImage = Convert.ToBase64String(x.Bytes),
                FileExtension = x.FileExtension,
                ImageId = x.ImageId,
                ProcessingStatus = x.ProcessingStatus,
                CreatedOnUtc = x.CreatedOnUtc,
                EditedAtUtc = x.EditedAtUtc
            })
            .ToList();

        var response = new GetThumbnailsResponse
        {
            Thumbnails = dtos
        };

        return Results.Ok(response);
    }
}
