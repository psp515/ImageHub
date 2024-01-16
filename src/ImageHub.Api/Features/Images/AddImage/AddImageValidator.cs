using FluentValidation;

namespace ImageHub.Api.Features.Images.AddImage;

public class AddImageValidator : AbstractValidator<AddImageCommand>
{
    public AddImageValidator()
    {
        
    }
}
