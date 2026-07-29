using MassTransit;
using Microsoft.Extensions.Logging;
using NotificationService.Application.Abstractions;
using Shared.Abstractions.Events;

namespace NotificationService.Application.Consumers;

internal sealed class SensorDataArrivedEventConsumer(
    INotificationPublisher notificationPublisher,
    ILogger<SensorDataArrivedEventConsumer> logger
) : IConsumer<SensorDataArrivedEvent>
{
    public async Task Consume(ConsumeContext<SensorDataArrivedEvent> context)
    {
        await notificationPublisher.Notify();
        logger.LogInformation("Notification sent");
    }
}
