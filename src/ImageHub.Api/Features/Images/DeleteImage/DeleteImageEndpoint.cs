
using ImageHub.Api.Extensions;

namespace ImageHub.Api.Features.Images.DeleteImage;

public class DeleteImageEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/images/{id}", async (string id, ISender sender) =>
        {
            var query = new GetImageQuery { Id = id };

            var result = await sender.Send(query);    

            return result.IsSuccess ? Results.Ok(result) : result.ToResultsDetails();

        }).WithGroupName(ImagesExtensions.Name);
    }
}
