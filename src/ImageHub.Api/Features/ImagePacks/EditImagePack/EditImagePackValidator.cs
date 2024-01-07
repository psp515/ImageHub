using FluentValidation;

namespace ImageHub.Api.Features.ImagePacks.AddImagePack;

public class EditImagePackValidator : AbstractValidator<EditImagePackCommand>
{
    public EditImagePackValidator()
    {
        RuleFor(x => x.Description)
            .MaximumLength(500);
    }
}