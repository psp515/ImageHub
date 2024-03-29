﻿using FluentValidation;
using ImageHub.Api.Features.Images.AddImage;
using ImageHub.Api.Infrastructure.Behaviors;
using ImageHub.Api.Infrastructure.MessageBroker;
using ImageHub.Api.Infrastructure.Middlewares;
using ImageHub.Api.Infrastructure.Persistence;
using ImageHub.Api.Infrastructure.Services;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
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

        builder.Services.AddValidatorsFromAssembly(assembly);

        builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
        builder.Services.AddTransient<TimingMiddleware>();

        builder.Services.AddDistributedMemoryCache();
        builder.Services.AddSingleton<ICacheService, CacheService>();

        builder.Services.AddHostedService<ProcessingMonitorService>();

        builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(assembly));
        builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));
        builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

        builder.Host.UseSerilog((context, configuration) 
            => configuration.ReadFrom.Configuration(context.Configuration));

        builder.Services.Configure<MessageBrokerSettings>(
            builder.Configuration.GetSection("MessageBroker"));

        builder.Services.AddSingleton(sp 
            => sp.GetRequiredService<IOptions<MessageBrokerSettings>>().Value);

        builder.Services.AddMassTransit(bus => {

            bus.SetKebabCaseEndpointNameFormatter();

            bus.AddConsumer<AddImageEventConsumer>();

            bus.UsingRabbitMq((context,config) => {                 
                
                var settings = context.GetRequiredService<MessageBrokerSettings>();

                config.ReceiveEndpoint("addimage", endpoint =>
                {
                    endpoint.ConfigureConsumer<AddImageEventConsumer>(context);
                });

                config.Host(new Uri(settings.Host), host =>
                {
                    host.Username(settings.Username);
                    host.Password(settings.Password);
                });
            });
        });
        builder.Services.AddTransient<IEventBus, EventBus>();

        builder.Services.AddSignalR();

        return builder;
    }

    public static WebApplication UseInfrastructure(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        else
        {
            app.UseHttpsRedirection();
        }

        app.UseMiddleware<TimingMiddleware>();
        app.UseExceptionHandler();
        app.UseRouting();
        app.MapCarter();

        return app;
    }
}
