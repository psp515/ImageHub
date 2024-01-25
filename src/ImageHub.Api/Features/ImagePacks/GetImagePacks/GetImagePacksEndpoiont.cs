using ImageHub.Api.Contracts.ImagePacks.AddImagePack;
using Microsoft.AspNetCore.Mvc;

namespace ImageHub.Api.Features.ImagePacks.GetImagePack;

public class GetImagePacksEndpoiont : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/imagepacks", Get)
            .WithTags(ImagePacksExtensions.Name);
    }

    [ProducesResponseType(typeof(AddImagePackResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IResult> Get(ISender service, int page = 1, int size = 25)
    {
        var query = new GetImagePacksQuery 
        {
            Page = page,
            Size = size
        };

        var result = await service.Send(query);

        return result.IsSuccess ? Results.Ok(result.Value) : result.ToResultsDetails();
    }
}
