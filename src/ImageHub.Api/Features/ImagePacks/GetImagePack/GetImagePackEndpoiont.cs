namespace ImageHub.Api.Features.ImagePacks.GetImagePack;

public class GetImagePackEndpoiont : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/imagepack/{imagePackId}", async (Guid imagePackId, ISender service) =>
        {
            var query = new GetImagePackQuery { Id = imagePackId };

            var result = await service.Send(query);

            if (result.IsFailure)
                return Results.NotFound(result.Error);

            return Results.Ok(result.Value);
        });
    }
}
