using DataProcessorService.Application.Consumers;
using MassTransit;
using MassTransit.KafkaIntegration;
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
            x.UsingInMemory();

            x.AddRider(rider =>
            {
                rider.AddConsumer<SensorDataArrivedEventConsumer>();

                rider.UsingKafka((context, k) =>
                {
                    k.Host("localhost:9092");

                    k.TopicEndpoint<SensorDataArrivedEvent>(
                        kafkaSettings.Topics.SensorsDataTopic,
                        kafkaSettings.Groups.DataProcessorServiceGroup,
                        e =>
                    {
                        e.ConfigureConsumer<SensorDataArrivedEventConsumer>(context);
                    });
                });
            });
        });
    }
}
