using FluentValidation;
using ImageHub.Api.Behaviors;
using ImageHub.Api.Features.ImagePacks;
using ImageHub.Api.Features.Images.Repositories;
using ImageHub.Api.Infrastructure;
using ImageHub.Api.Infrastructure.Persistence;
using ImageHub.Api.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
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
builder.Services.AddAntiforgery(options => options.FormFieldName = "csrfToken");

builder.Services.AddScoped<IImagePackRepository, ImagePackRepository>();
builder.Services.AddScoped<IImageRepository, ImageRepository>();
builder.Services.AddScoped<IImageStoreRepository, FileRepository>();
builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));

builder.Services.AddCors(options =>
{
    options.AddPolicy("Allow",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod());
});

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("Allow");
app.UseHttpsRedirection();
app.UseExceptionHandler();
app.UseRouting();
app.UseAntiforgery();
app.MapCarter();
app.Run();

public partial class Program { }