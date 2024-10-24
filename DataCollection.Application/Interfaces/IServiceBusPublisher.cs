namespace DataCollection.Application.Interfaces;

public interface IServiceBusPublisher
{
    Task PublishMessageAsync<T>(T message, string queueName, CancellationToken cancellationToken = default);
}
