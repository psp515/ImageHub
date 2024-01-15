using FluentValidation;

namespace ImageHub.Api.Features.Images.AddImage;

public class AddImageValidator : AbstractValidator<AddImageCommand>
{
    public AddImageValidator()
    {
        RuleFor(x => x.Image).NotNull().WithMessage("Image is required.");
        RuleFor(x => x.Image).Must(x => x.Length > 0).WithMessage("Image is required.");
        RuleFor(x => x.Image).Must(x => x.Length < 5_000_000).WithMessage("Image must be less than 5MB.");
    }
}
