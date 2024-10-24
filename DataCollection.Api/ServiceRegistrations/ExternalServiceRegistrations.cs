using DataCollection.Contracts.ExternalServices;
using DataCollection.Infrastructure.ExternalApis;

namespace DataCollection.Api.ServiceRegistrations;

public static class ExternalServiceRegistrations
{
    public static IServiceCollection AddExternalServices(this IServiceCollection services)
    {
        services.AddHttpClient<ISlackClient, SlackClient>(client =>
        {
            client.BaseAddress = new Uri("https://hooks.slack.com");
        });

        services.AddHttpClient<IF1Client, F1Client>(client =>
        {
            client.BaseAddress = new Uri("https://www.formula1.com");
        });

        return services;
    }
}
