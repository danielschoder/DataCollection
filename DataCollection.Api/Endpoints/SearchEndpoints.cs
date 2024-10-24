using DataCollection.Application.Commands;
using DataCollection.Contracts.Requests;
using MediatR;

namespace DataCollection.Api.Endpoints;

public static class SearchEndpoints
{
    public static void MapSearchEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/races", ImportRacesAsync);
        app.MapPost("/api/results", ImportResultsAsync);

        static async Task<IResult> ImportRacesAsync(SearchRacesRequest request, IMediator mediator)
            => Results.Ok(await mediator.Send(new SearchRacesCommand(request)));

        static async Task<IResult> ImportResultsAsync(SearchResultsRequest request, IMediator mediator)
            => Results.Ok(await mediator.Send(new SearchResultsCommand(request)));
    }
}
