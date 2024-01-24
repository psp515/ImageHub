using ImageHub.Api.Contracts.Image.UpdateImage;
using ImageHub.Api.Extensions;
using MediatR;

namespace ImageHub.Api.Features.Images.UpdateImage;

public class UpdateImageEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPatch("/api/images/{id:guid}", Update).WithTags(ImagesExtensions.Name);
    }

    public async Task<IResult> Update(Guid id,
            UpdateImageRequest request,
            ISender sender)
    {
        var command = new UpdateImageCommand
        {
            Id = id,
            Description = request.Description
        };

        var result = await sender.Send(command);

        if (result.IsFailure)
            return result.ToResultsDetails();
        
        return Results.Ok(result.Value);
    }
}
