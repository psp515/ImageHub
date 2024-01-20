using FluentValidation;

namespace ImageHub.Api.Features.Images.GetImageFile;

public class GetImageQueryValidator :AbstractValidator<GetImageFileQuery>
{
    public GetImageQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .Must(x => Guid.TryParse(x, out _))
            .WithMessage("Provided id is not valid id.");
    }
}
