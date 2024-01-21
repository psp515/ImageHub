using FluentValidation;
using ImageHub.Api.Features.Images.DeteleImage;

namespace ImageHub.Api.Features.Images.DeleteImage;

public class DeleteImageValidator : AbstractValidator<DeleteImageCommand>
{
    public DeleteImageValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .Must(x => Guid.TryParse(x, out _))
            .WithMessage("Provided id is not valid id.");
    }
}