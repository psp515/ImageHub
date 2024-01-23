using ImageHub.Api.Contracts.Image.GetImages;
using ImageHub.Api.Extensions;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace ImageHub.Api.Features.Images.GetImagesByPack;

public class GetImagesEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/images", async (ISender sender,
            [FromQuery(Name = "pack")] Guid packId = default,
            [FromQuery(Name = "page")] int page = 1,
            [FromQuery(Name = "size")] int size = 25) =>
        {
            var query = new GetImagesQuery
            {
                PackId = packId,
                Page = page,
                Size = size
            };

            var result = await sender.Send(query);

            if (result.IsFailure)
                return result.ToResultsDetails();

            var dtos = result.Value
                .Select(x => x.Adapt<ImageDto>())
                .ToList(); 

            var response = new GetImagesResponse
            {
                Images = dtos
            };

            return Results.Ok(result.Value);
        }).WithGroupName(ImagesExtensions.Name);
    }
}
