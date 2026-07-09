using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Shared.Abstractions.Events;

namespace DataIngestorService.Application;

public static class Injection
{
    public static IServiceCollection InjectApplication(this IServiceCollection services) =>
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Injection).Assembly));

    private static IServiceCollection AddKafkaWithMassTransit(this IServiceCollection services)
    {
        services.AddMassTransit(x =>
        {
            x.UsingInMemory((context, cfg) =>
            {
                cfg.ConfigureEndpoints(context);
            });

            x.AddRider(rider =>
            {
                rider.AddProducer<SensorDataArrivedEvent>(
                    "sensors-data-arrived");

                rider.UsingKafka((context, k) =>
                {
                    //k.Host(configuration["Kafka:BootstrapServers"]);
                });
            });
        });

        return services;
    }
}
