using FluentValidation;
using ImageHub.Api.Infrastructure.Behaviors;
using ImageHub.Api.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace ImageHub.Api.Infrastructure;

public static class InfrastructureExtnesions
{
    public static WebApplicationBuilder RegisterInfrastructureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddProblemDetails();

        var assembly = typeof(Program).Assembly;

        builder.Services.AddDbContext<ApplicationDbContext>(options
            => options.UseNpgsql(builder.Configuration.GetConnectionString("Database")));

        builder.Services.AddCarter();
        builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(assembly));
        builder.Services.AddValidatorsFromAssembly(assembly);
        builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

        builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));
        builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

        builder.Host.UseSerilog((context, configuration) 
            => configuration.ReadFrom.Configuration(context.Configuration));

        return builder;
    }

    public static WebApplication UseInfrastructure(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseExceptionHandler();
        app.UseRouting();
        app.MapCarter();
        return app;
    }
}
