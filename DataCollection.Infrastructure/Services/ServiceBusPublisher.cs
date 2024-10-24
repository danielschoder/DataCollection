using Azure.Messaging.ServiceBus;
using DataCollection.Application.Interfaces;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DataCollection.Infrastructure.Services;

public class ServiceBusPublisher(ServiceBusClient serviceBusClient) : IServiceBusPublisher
{
    private readonly ServiceBusClient _serviceBusClient = serviceBusClient;
    private static readonly JsonSerializerOptions _options
        = new() { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };
    
    public async Task PublishMessageAsync<T>(T message, string queueName, CancellationToken cancellationToken = default)
    {
        var sender = _serviceBusClient.CreateSender(queueName);
        var messageBody = JsonSerializer.Serialize(message, _options);
        await sender.SendMessageAsync(new ServiceBusMessage(messageBody), cancellationToken);
    }
}
