using FluentValidation;

namespace ImageHub.Api.Features.Images.AddImage;

public class AddImageValidator : AbstractValidator<AddImageCommand>
{
    private static readonly string[] AllowedFileTypes = 
    [ 
        "image/jpeg", 
        "image/png"
    ];

    private static readonly int MaxKiloBytes = 64;
    private static readonly int BytesInKiloByte = 1024;

    public AddImageValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.Image.Length)
            .LessThan(MaxKiloBytes * BytesInKiloByte);

        RuleFor(x => x.Description)
            .MaximumLength(500);

        RuleFor(x => x.Image)
            .NotNull();

        RuleFor(x => x.FileType)
            .Must(x => AllowedFileTypes.Contains(x))
            .WithMessage($"Invalid file type. Allowed types: {string.Join(',',AllowedFileTypes)}");
    }
}
