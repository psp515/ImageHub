using ImageHub.Api.Contracts.Image.AddImage;
using ImageHub.Api.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace ImageHub.Api.Features.Images.AddImage;

public class AddImageEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/images", async (AddImageRequest request, ISender sender, 
            [FromQuery(Name = "pack")] string packId = "") =>
        {
            var command = new AddImageCommand
            {
                Name = request.Name,
                Description = request.Description,
                FileExtension = request.FileExtension.FileTypeFromName(),
                GroupId = Guid.TryParse(packId, out Guid pack) ? pack : null,
                Image = request.Image
            };

            var result = await sender.Send(command);

            if (result.IsFailure)
                return result.ToResultsDetails();

            var response = new AddImageResponse { 
                Id = result.Value.Id
            };

            return Results.Created($"api/images/{result.Value.Id}",response);
        }).WithName(ImagesExtensions.Name);
    }
}
