﻿using FluentValidation;
using ImageHub.Api.Contracts.Image.AddImage;
using ImageHub.Api.Contracts.Image.GetImage;
using ImageHub.Api.Features.Images.AddImage;
using ImageHub.Api.Features.Images.Repositories;

namespace ImageHub.Api.Features.Images.GetImage;

public class GetImageHandler(IImageRepository imageRepository, IValidator<GetImageQuery> validator) : IRequestHandler<GetImageQuery, Result<GetImageResponse>>
{
    public async Task<Result<GetImageResponse>> Handle(GetImageQuery request, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            var error = GetImageErrors.ValidationFailed(validationResult);
            return Result<GetImageResponse>.Failure(error);
        }

        var guid = Guid.Parse(request.Id);
        var image = await imageRepository.GetImageById(guid, cancellationToken);

        if (image is null)
        {
            var error = GetImageErrors.ImageNotFound;
            return Result<GetImageResponse>.Failure(error);
        }


        return null;
    }
}
