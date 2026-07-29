using DataProcessorService.Application.Consumers;
using Mapster;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Shared.Abstractions.Events;
using Shared.Settings.Kafka;
using System.Reflection;

namespace DataProcessorService.Application;

public static class Injection
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services, KafkaSettings kafkaSettings)
    {
        return services
            .AddMapping()
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
                rider.AddConsumer<SensorDataArrivedEventConsumer>();

                rider.UsingKafka((context, k) =>
                {
                    k.Host(kafkaSettings.Host);

                    k.TopicEndpoint<SensorDataArrivedEvent>(
                        kafkaSettings.Topics.SensorsDataTopic,
                        kafkaSettings.Groups.DataProcessorServiceGroup,
                        e =>
                    {
                        e.CreateIfMissing();
                        e.ConfigureConsumer<SensorDataArrivedEventConsumer>(context);
                    });
                });
            });
        });
    }

    private static IServiceCollection AddMapping(this IServiceCollection services)
    {
        services.AddMapster();

        return services;
    }
}
