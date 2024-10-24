using DataCollection.Contracts.Requests;
using DataCollection.Contracts.Responses;
using MediatR;

namespace DataCollection.Application.Commands;

public class SearchRacesCommand(SearchRacesRequest importResultsRequest) : IRequest<SearchResponse>
{
    public int Year { get; set; } = importResultsRequest.Year;
}
