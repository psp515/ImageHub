namespace ImageHub.Api.Features.Ping.Get;

public class PingEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/ping/{name}", async (string name, ISender service) =>
        {
            var query = new PingQuery { Name = name };

            var result = await service.Send(query);

            if (result.IsFailure)
                return Results.BadRequest(result.Error);

            return Results.Ok(result.Value);
        });
    }
}
