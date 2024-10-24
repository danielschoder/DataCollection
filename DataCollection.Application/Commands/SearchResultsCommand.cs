using DataCollection.Contracts.Requests;
using DataCollection.Contracts.Responses;
using MediatR;

namespace DataCollection.Application.Commands;

public class SearchResultsCommand(SearchResultsRequest importResultsRequest) : IRequest<SearchResponse>
{
    public int Year { get; set; } = importResultsRequest.Year;
    public int Round { get; set; } = importResultsRequest.Round;
}
