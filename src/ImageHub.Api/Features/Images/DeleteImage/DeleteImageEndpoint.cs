
using ImageHub.Api.Extensions;
using ImageHub.Api.Features.Images.DeteleImage;

namespace ImageHub.Api.Features.Images.DeleteImage;

public class DeleteImageEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/images/{id}", async (string id, ISender sender) =>
        {
            var query = new DeleteImageCommand { Id = id };

            var result = await sender.Send(query);    

            return result.IsSuccess ? Results.Ok(result) : result.ToResultsDetails();

        }).WithGroupName(ImagesExtensions.Name);
    }
}
