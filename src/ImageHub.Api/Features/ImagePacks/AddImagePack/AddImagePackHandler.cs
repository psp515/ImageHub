using FluentValidation;
using ImageHub.Api.Infrastructure.Persistence;

namespace ImageHub.Api.Features.ImagePacks.AddImagePack;

public class AddImagePackHandler : IRequestHandler<AddImagePackCommand, Result<Guid>>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IValidator<AddImagePackCommand> _validator;
    
    public AddImagePackHandler(ApplicationDbContext dbContext, IValidator<AddImagePackCommand> validator)
    {
        _dbContext = dbContext;
        _validator = validator;
    }

    public async Task<Result<Guid>> Handle(AddImagePackCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            var error = AddImagePackErrors.ValidationFailed(validationResult);
            return Result<Guid>.Failure(error);
        }
        
        var imagePack = new ImagePack
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Description = request.Description,
            CreatedOnUtc = DateTime.UtcNow,
            EditedAtUtc = DateTime.UtcNow
        };

        _dbContext.Add(imagePack);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result<Guid>.Success(imagePack.Id);
    }
}
