using ImageHub.Api.Extensions;

namespace ImageHub.Api.Features.ImagePacks.GetImagePack;

public class GetAntiforgeryEndpoiont : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/imagepacks/{imagePackId}", async (Guid imagePackId, ISender service) =>
        {
            var query = new GetImagePackQuery { Id = imagePackId };

            var result = await service.Send(query);

            return result.IsSuccess ? Results.Ok(result.Value) : result.ToResultsDetails();
        }).WithTags(ImagePacksExtensions.Name);
    }
}
