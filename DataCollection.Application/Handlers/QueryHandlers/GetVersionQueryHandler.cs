using DataCollection.Application.Interfaces;
using DataCollection.Application.Queries;
using DataCollection.Contracts.Responses;
using DataCollection.Domain.Common.Interfaces;
using MediatR;
using System.Reflection;

namespace DataCollection.Application.Handlers.QueryHandlers;

public class GetVersionQueryHandler(
    IDateTimeProvider dateTimeProvider,
    IScopedLogService logService)
    : IRequestHandler<GetVersionQuery, AliveResponse>
{
    private readonly IDateTimeProvider _dateTimeProvider = dateTimeProvider;
    private readonly IScopedLogService _logService = logService;

    public Task<AliveResponse> Handle(GetVersionQuery request, CancellationToken cancellationToken)
    {
        _logService.Log();
        var alive = new AliveResponse
        {
            UtcNow = _dateTimeProvider.UtcNow,
            Version = Assembly.GetEntryAssembly().GetName().Version.ToString()
        };
        _logService.Log(alive.Version, nameof(alive.Version));
        return Task.FromResult(alive);
    }
}
