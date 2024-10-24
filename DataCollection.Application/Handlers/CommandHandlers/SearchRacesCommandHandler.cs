using DataCollection.Application.Commands;
using DataCollection.Application.Interfaces;
using DataCollection.Contracts.ExternalServices;
using DataCollection.Contracts.F1Dtos;
using DataCollection.Contracts.Responses;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace DataCollection.Application.Handlers.CommandHandlers;

public class SearchRacesCommandHandler(
    IConfiguration configuration,
    IF1Client formula1Client,
    IServiceBusPublisher serviceBusPublisher)
    : IRequestHandler<SearchRacesCommand, SearchResponse>
{
    private readonly string _queueName = configuration["ServiceBus:QueueName"];
    private readonly IF1Client _formula1Client = formula1Client;
    private readonly IServiceBusPublisher _serviceBusPublisher = serviceBusPublisher;

    public async Task<SearchResponse> Handle(SearchRacesCommand request, CancellationToken cancellationToken)
    {
        var data = new F1Data
        {
            Races = await _formula1Client.GetRacesAsync(request.Year)
        };

        await _serviceBusPublisher.PublishMessageAsync(data, _queueName, cancellationToken);

        return new SearchResponse(nameof(SearchRacesCommand), request.Year, data.Races.Length);
    }
}
