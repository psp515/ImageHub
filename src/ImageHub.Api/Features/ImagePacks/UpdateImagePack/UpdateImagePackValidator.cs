using FluentValidation;

namespace ImageHub.Api.Features.ImagePacks.AddImagePack;

public class UpdateImagePackValidator : AbstractValidator<UpdateImagePackCommand>
{
    public UpdateImagePackValidator()
    {
        RuleFor(x => x.Description)
            .MaximumLength(500);
    }
}