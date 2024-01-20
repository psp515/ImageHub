using FluentValidation;
using ImageHub.Api.Contracts.Image.DeleteImage;

namespace ImageHub.Api.Features.Images.DeleteImage;

public class DeleteImageValidator : AbstractValidator<DeleteImageResponse>
{
    public DeleteImageValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .Must(x => Guid.TryParse(x, out _))
            .WithMessage("Provided id is not valid id.");
    }
}