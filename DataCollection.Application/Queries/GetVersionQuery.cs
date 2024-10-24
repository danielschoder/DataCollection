using DataCollection.Contracts.Responses;
using MediatR;

namespace DataCollection.Application.Queries;

public class GetVersionQuery : IRequest<AliveResponse> { }
