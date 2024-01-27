namespace ImageHub.Api.Features.Security;

public static class SecurityExtensions
{
    public static string Name => "Security Controller";

    public static WebApplicationBuilder RegisterSecurityServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddAntiforgery(options => options.FormFieldName = "csrfToken");
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("Allow",
                builder => builder.AllowAnyOrigin()
                                  .AllowAnyHeader()
                                  .AllowAnyMethod());
        });

        return builder;
    }

    public static WebApplication UseSecurity(this WebApplication app)
    {
        app.UseCors("Allow");
        app.UseAntiforgery();
        return app;
    }
}
