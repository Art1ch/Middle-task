using Confluent.Kafka;
using DataProcessorService.Application.Consumers;
using MassTransit;
using MassTransit.KafkaIntegration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Abstractions.Events;
using Shared.Settings.Kafka;
using System.Net;
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
                rider.AddConsumer<SensorDataArrivedEventConsumer>(cfg =>
                {
                    cfg.Options<BatchOptions>(options => options
                        .SetMessageLimit(100)
                        .SetTimeLimit(s: 1)
                        .SetTimeLimitStart(BatchTimeLimitStart.FromLast)
                        .GroupBy<SensorDataArrivedEvent, Guid>(x => x.MessageId)
                        .SetConcurrencyLimit(10));
                });

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
}
