using ImageHub.Api.Features.ImagePacks;
using ImageHub.Api.Features.Images;
using ImageHub.Api.Features.Security;
using ImageHub.Api.Features.Thumbnails;
using ImageHub.Api.Infrastructure;

var builder = WebApplication.CreateBuilder(args)
    .RegisterInfrastructureServices()
    .RegisterSecurityServices()
    .RegisterImagePacksServices()
    .RegisterImagesServices()
    .RegisterThumbnailServices();

var app = builder
    .Build()
    .UseInfrastructure()
    .UseSecurity();

app.Run();

public partial class Program { }