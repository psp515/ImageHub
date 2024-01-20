
using ImageHub.Api.Extensions;

namespace ImageHub.Api.Features.Images.GetImageFile;

public class GetImageFileEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/images/{id}/file", async(string id, ISender sender) =>
        {
            var query = new GetImageFileQuery { Id=id};

            var result = await sender.Send(query);

            if (result.IsFailure)
                return result.ToResultsDetails();

            return Results.File(result.Value.Bytes, contentType: 
                result.Value.FileType, 
                lastModified: result.Value.EditedAtUtc);
        });
    }
}
