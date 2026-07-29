using Microsoft.AspNetCore.SignalR;
using NotificationService.Api.Hubs;
using NotificationService.Application.Abstractions;

namespace NotificationService.Api.Implementations;

public class SignalRNotificationPublisher : INotificationPublisher
{
    private readonly IHubContext<SensorHub> _hub;

    public SignalRNotificationPublisher(IHubContext<SensorHub> hub)
    {
        _hub = hub;
    }

    public async Task Notify()
    {
        await _hub.Clients.All.SendAsync("sensor-data", "New sensors data arrived");
    }
}
