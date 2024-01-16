using FluentValidation;
using ImageHub.Api.Features.ImagePacks;

namespace ImageHub.Api.Features.Images.AddImage;

public class AddImageValidator : AbstractValidator<AddImageCommand>
{
    public AddImageValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.Description)
            .MaximumLength(500);

        RuleFor(x => x.Image)
            .NotNull();

        RuleFor(x => x.FileExtension)
            .Must(x => x != FileTypes.Invalid);

    }
}
