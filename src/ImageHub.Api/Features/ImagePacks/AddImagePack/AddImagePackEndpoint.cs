using ImageHub.Api.Contracts.ImagePacks.AddImagePack;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace ImageHub.Api.Features.ImagePacks.AddImagePack;

public class AddImagePackEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/imagepacks", Add)
            .WithTags(ImagePacksExtensions.Name);
    }

    [ProducesResponseType(typeof(AddImagePackResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IResult> Add(AddImagePackRequest request, ISender sender)
    {
        var command = request.Adapt<AddImagePackCommand>();

        var result = await sender.Send(command);

        if (result.IsFailure)
        {
            return result.ToResultsDetails();
        }

        var response = new AddImagePackResponse(result.Value);

        return Results.Created($"/api/imagepack/{result.Value}", response);
    }
}
