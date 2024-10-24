using Azure.Messaging.ServiceBus;
using DataCollection.Application.Interfaces;
using DataCollection.Domain.Common.Interfaces;
using DataCollection.Infrastructure.Services;

namespace DataCollection.Api.ServiceRegistrations;

public static class InfrastructureServiceRegistrations
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IWebHostEnvironment environment,
        IConfiguration configuration)
    {
        var isDevelopment = environment.IsDevelopment();

        services.AddHttpContextAccessor();

        services.AddSingleton<IHtmlScraper, HtmlScraper>();
        services.AddSingleton(serviceProvider =>
        {
            var connectionString = configuration["ServiceBus:ConnectionString"];
            return new ServiceBusClient(connectionString);
        });

        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddScoped<IScopedErrorService, ScopedErrorService>();
        services.AddScoped<IScopedLogService, ScopedLogService>();
        services.AddScoped<IServiceBusPublisher, ServiceBusPublisher>();

        services.AddScoped(typeof(IExceptionService),
            isDevelopment ? typeof(ExceptionInDevelopmentService) : typeof(ExceptionInProductionService));

        return services;
    }
}
