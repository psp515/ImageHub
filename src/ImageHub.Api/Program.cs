using ImageHub.Api.Features.ImagePacks;
using ImageHub.Api.Features.Images;
using ImageHub.Api.Features.Security;
using ImageHub.Api.Features.Thumbnails;
using ImageHub.Api.Infrastructure;
using ImageHub.Api.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args)
    .RegisterInfrastructureServices()
    .RegisterSecurityServices()
    .RegisterImagePacksServices()
    .RegisterImagesServices()
    .RegisterThumbnailServices();

var app = builder
    .Build()
    .UseInfrastructure()
    .UseSecurity()
    .UseThumbnail();

if (builder.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    await dbContext.Database.EnsureCreatedAsync();
}

app.Run();

public partial class Program { }