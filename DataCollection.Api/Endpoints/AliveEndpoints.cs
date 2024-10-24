using DataCollection.Application.Interfaces;
using DataCollection.Application.Queries;
using MediatR;

namespace DataCollection.Api.Endpoints;

public static class AliveEndpoints
{
    public static void MapAliveEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/", GetVersionAsync);
        app.MapGet("/api", GetVersionAsync);
        app.MapGet("/error", ThrowError);

        static async Task<IResult> GetVersionAsync(IMediator mediator)
            => Results.Ok(await mediator.Send(new GetVersionQuery()));

        static IResult ThrowError(IScopedLogService logService)
        {
            logService.Log("Before error");
            var zero = 0;
            var y = 1 / zero;
            logService.Log("After error");
            return Results.Ok();
        }
    }
}
