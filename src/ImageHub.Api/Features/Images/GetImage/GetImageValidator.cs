using FluentValidation;

namespace ImageHub.Api.Features.Images.GetImage;

public class GetImageValidator : AbstractValidator<GetImageQuery>
{
    public GetImageValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .Must(x => Guid.TryParse(x, out _))
            .WithMessage("Provided id is not valid id.");
    }
}