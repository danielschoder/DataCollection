using DataCollection.Application.Commands;
using DataCollection.Application.Interfaces;
using DataCollection.Contracts.ExternalServices;
using DataCollection.Contracts.F1Dtos;
using DataCollection.Contracts.Responses;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace DataCollection.Application.Handlers.CommandHandlers;

public class SearchResultsCommandHandler(
    IConfiguration configuration,
    IF1Client formula1Client,
    IServiceBusPublisher serviceBusPublisher)
    : IRequestHandler<SearchResultsCommand, SearchResponse>
{
    private readonly string _queueName = configuration["ServiceBus:QueueName"];
    private readonly IF1Client _formula1Client = formula1Client;
    private readonly IServiceBusPublisher _serviceBusPublisher = serviceBusPublisher;

    public async Task<SearchResponse> Handle(SearchResultsCommand request, CancellationToken cancellationToken)
    {
        var raceResults = await _formula1Client.GetRaceResultsAsync(request.Year, request.Round);
        var data = new F1Data
        {
        };

        await _serviceBusPublisher.PublishMessageAsync(raceResults, _queueName, cancellationToken);

        return new SearchResponse(nameof(SearchRacesCommand), request.Year, data.Races[0].Results.Length);
    }
}
