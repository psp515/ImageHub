using ImageHub.Api.Contracts.Image.GetImage;
using Microsoft.AspNetCore.Mvc;

namespace ImageHub.Api.Features.Images.GetImageFile;

public class GetImageFileEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/images/{id:guid}/file", Get)
            .WithTags(ImagesExtensions.Name);
    
    }

    [ProducesResponseType(typeof(GetImageResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IResult> Get(Guid id, ISender sender)
    {
        var query = new GetImageFileQuery { Id=id };

        var result = await sender.Send(query);

        if (result.IsFailure)
            return result.ToResultsDetails();

        return Results.File(result.Value.Bytes, contentType:
            result.Value.FileType,
            lastModified: result.Value.EditedAtUtc);
    }
}
