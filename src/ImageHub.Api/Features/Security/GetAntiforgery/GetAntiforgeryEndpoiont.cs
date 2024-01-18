using ImageHub.Api.Contracts.Security.GetAntiforgery;
using Microsoft.AspNetCore.Antiforgery;

namespace ImageHub.Api.Features.Security.GetAmtiforgery;

public class GetAntiforgeryEndpoiont : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/security/antiforgery/token", async (HttpContext httpContext, IAntiforgery antiforgery) =>
        {
            var tokens = antiforgery.GetAndStoreTokens(httpContext);

            var response = new GetAntiforgeryTokenResponse(tokens.RequestToken!);

            return Results.Ok(response);
        }).WithTags(SecurityExtensions.Name);
    }
}
