namespace ImageHub.Api.Features.ImagePacks.DeleteImagePack;

public class DeleteImagePackEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/api/imagepack/{id}", async (Guid id, ISender sender) =>
        {
            var command = new DeleteImagePackCommand { Id = id };

            var result = await sender.Send(command);

            if (result.IsFailure)
            {
                return Results.BadRequest(result.Error);
            }

            return Results.NoContent();
        });
    }
}
