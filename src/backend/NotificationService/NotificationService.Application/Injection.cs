using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using NotificationService.Application.Consumers;
using Shared.Abstractions.Events;
using Shared.Settings.Kafka;

namespace DataProcessorService.Application;

public static class Injection
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services, KafkaSettings kafkaSettings)
    {
        return services
            .AddMessageBroker(kafkaSettings);
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
                        kafkaSettings.Groups.NotificationServiceGroup,
                        e =>
                    {
                        e.CreateIfMissing();
                        e.ConfigureConsumer<SensorDataArrivedEventConsumer>(context);
                    });
                });
            });
        });
    }
}
