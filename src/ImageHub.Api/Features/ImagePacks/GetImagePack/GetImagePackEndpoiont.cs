using ImageHub.Api.Contracts.ImagePacks.AddImagePack;
using Microsoft.AspNetCore.Mvc;

namespace ImageHub.Api.Features.ImagePacks.GetImagePack;

public class GetAntiforgeryEndpoiont : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/imagepacks/{id:guid}", Get)
            .WithTags(ImagePacksExtensions.Name);
    }

    [ProducesResponseType(typeof(AddImagePackResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IResult> Get(Guid id, ISender service)
    {
        var query = new GetImagePackQuery { Id = id };

        var result = await service.Send(query);

        return result.IsSuccess ? Results.Ok(result.Value) : result.ToResultsDetails();
    }
}
