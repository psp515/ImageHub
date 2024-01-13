
using ImageHub.Api.Extensions;

namespace ImageHub.Api.Features.ImagePacks.GetImagePack;

public class GetImagePacksEndpoiont : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/imagepacks", async (ISender service) =>
        {
            var query = new GetImagePacksQuery();

            var result = await service.Send(query);

            return result.IsSuccess ? Results.Ok(result.Value) : result.ToResultsDetails();
        }).WithTags(ImagePacksExtensions.Name);
    }
}
