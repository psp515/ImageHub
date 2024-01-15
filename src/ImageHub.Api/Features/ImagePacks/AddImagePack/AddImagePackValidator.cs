using FluentValidation;

namespace ImageHub.Api.Features.ImagePacks.AddImagePack;

public class AddImagePackValidator : AbstractValidator<AddImagePackCommand>
{
    public AddImagePackValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.Description)
            .MaximumLength(500);
    }
}