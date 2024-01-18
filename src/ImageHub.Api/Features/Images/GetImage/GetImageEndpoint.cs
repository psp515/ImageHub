
namespace ImageHub.Api.Features.Images.GetImage;

public class GetImageEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/images/{id}", async (Guid id, ISender sender) =>
        {
            var app = new GetImageQuery { Id = null };

        }).WithGroupName(ImagesExtensions.Name);
    }
}
