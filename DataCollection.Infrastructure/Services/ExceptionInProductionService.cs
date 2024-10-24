﻿using DataCollection.Application.Interfaces;
using DataCollection.Contracts.ExternalServices;
using Microsoft.AspNetCore.Http;

namespace DataCollection.Infrastructure.Services;

public class ExceptionInProductionService(
    IHttpContextAccessor httpContext,
    IScopedLogService logService,
    ISlackClient slackClient)
    : ExceptionServiceBase(httpContext, logService), IExceptionService
{
    private readonly ISlackClient _slackClient = slackClient;

    public async Task HandleExceptionAsync(Exception exception)
    {
        await WriteResponse500Async(new { });

        _slackClient.SendMessage($":boom: EXCEPTION: {_logService.ExceptionAsTextBlock(exception)}");
    }
}