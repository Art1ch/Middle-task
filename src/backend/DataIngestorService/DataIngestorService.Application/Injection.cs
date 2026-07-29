using Mapster;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Shared.Abstractions.Events;
using Shared.Settings.Kafka;
using System.Reflection;

namespace DataIngestorService.Application;

public static class Injection
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services, KafkaSettings kafkaSettings)
    {
        return services
            .AddCommandsAndQueries()
            .AddMessageBroker(kafkaSettings);
    }

    private static IServiceCollection AddCommandsAndQueries(this IServiceCollection services)
    {
        return services.AddMediatR(x =>
        {
            x.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });
    }

    private static IServiceCollection AddMessageBroker(this IServiceCollection services, KafkaSettings kafkaSettings)
    {
        return services.AddMassTransit(x =>
        {
            x.UsingInMemory((context, cfg) =>
            {
                cfg.ConfigureEndpoints(context);
            });

            x.AddRider(rider =>
            {
                rider.AddProducer<SensorDataArrivedEvent>(kafkaSettings.Topics.SensorsDataTopic);

                rider.UsingKafka((context, k) =>
                {
                    k.Host(kafkaSettings.Host);
                });
            });
        });
    }
}
