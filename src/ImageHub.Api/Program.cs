using FluentValidation;
using ImageHub.Api.Behaviors;
using ImageHub.Api.Features.ImagePacks;
using ImageHub.Api.Infrastructure;
using ImageHub.Api.Infrastructure.Persistence;
using ImageHub.Api.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
var assembly = typeof(Program).Assembly;     

builder.Services.AddDbContext<ApplicationDbContext>(options 
    => options.UseNpgsql(builder.Configuration.GetConnectionString("Database")));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCarter();
builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(assembly));
builder.Services.AddValidatorsFromAssembly(assembly);
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddScoped<IImagePackRepository, ImagePackRepository>();
builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseExceptionHandler();

app.MapCarter();
app.Run();

public partial class Program { }