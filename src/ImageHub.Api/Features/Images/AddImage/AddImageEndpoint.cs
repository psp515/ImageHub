using ImageHub.Api.Contracts.Image.AddImage;
using ImageHub.Api.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace ImageHub.Api.Features.Images.AddImage;

public class AddImageEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/images", async (AddImageRequest request, ISender sender, 
            [FromQuery(Name = "groupId")] string groupId = "") =>
        {
            var command = new AddImageCommand
            {
                Name = request.Name,
                Description = request.Description,
                FileExtension = request.FileExtension,
                GroupId = Guid.Parse(groupId),
                Image = request.Image
            };

            var result = await sender.Send(command);

            if (result.IsFailure)
                return result.ToResultsDetails();

            var response = new AddImageResponse { 
                Id = result.Value.Id
            };

            return Results.Created($"api/images/{result.Value.Id}",response);
        });
    }
}
