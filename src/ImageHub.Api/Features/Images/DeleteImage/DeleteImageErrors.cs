﻿using FluentValidation.Results;

namespace ImageHub.Api.Features.Images.DeleteImage;

public class DeleteImageErrors
{
    public static Error ImageNotFound
        => Error.NotFound("Image.Delete.NotFound");
}
