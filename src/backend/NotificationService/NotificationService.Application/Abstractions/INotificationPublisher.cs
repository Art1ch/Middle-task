namespace NotificationService.Application.Abstractions;

public interface INotificationPublisher
{
    Task Notify();
}
