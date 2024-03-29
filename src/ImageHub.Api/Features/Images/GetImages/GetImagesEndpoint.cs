﻿using ImageHub.Api.Contracts.Image.GetImages;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace ImageHub.Api.Features.Images.GetImagesByPack;

public class GetImagesEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/images", Get)
            .WithTags(ImagesExtensions.Name);
    }

    [ProducesResponseType(typeof(GetImagesResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IResult> Get(ISender sender,
            Guid? packId = null,
            int page = 1, int
            size = 25)
    {
        var query = new GetImagesQuery
        {
            PackId = packId,
            Page = page,
            Size = size
        };

        var result = await sender.Send(query);

        if (result.IsFailure)
            return result.ToResultsDetails();

        var dtos = result.Value
            .Select(x => x.Adapt<ImageDto>())
            .ToList();

        var response = new GetImagesResponse
        {
            Images = dtos
        };

        return Results.Ok(response);
    }
}
