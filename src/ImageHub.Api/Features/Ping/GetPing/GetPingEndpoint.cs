using ImageHub.Api.Extensions;

namespace ImageHub.Api.Features.Ping.Get;

public class GetPingEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/ping/{name}", async (string name, ISender service) =>
        {
            var query = new GetPingQuery { Name = name };

            var result = await service.Send(query);

            return result.IsSuccess ? Results.Ok(result.Value) : result.ToResultsDetails();
        });
    }
}
