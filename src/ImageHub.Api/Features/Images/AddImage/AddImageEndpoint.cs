using ImageHub.Api.Contracts.Image.AddImage;
using ImageHub.Api.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace ImageHub.Api.Features.Images.AddImage;

public class AddImageEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/images", async (ISender sender, 
            [FromForm] AddImageRequest request,
            [FromQuery(Name = "pack")] string packId = "") =>
        {
            var command = new AddImageCommand
            {
                Name = request.Name,
                Description = request.Description,
                FileType = request.Image.ContentType,
                PackId = Guid.TryParse(packId, out Guid pack) ? pack : null,
                Image = request.Image
            };

            var result = await sender.Send(command);

            if (result.IsFailure)
                return result.ToResultsDetails();

            return Results.Created($"api/images/{result.Value.Id}", result.Value);
        }).WithGroupName(ImagesExtensions.Name);
    }
}
