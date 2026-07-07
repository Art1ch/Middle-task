using MediatR;
using Shared.Abstractions.Events;
using Shared.Abstractions.Messaging;

namespace DataIngestorService.Application.Commands.PublishSensorData;

internal sealed class PublishSensorsDataCommandHandler(
    IEventPublisher<SensorDataArrivedEvent> eventPublisher
) : IRequestHandler<PublishSensorsDataCommand, PublishSensorsDataCommandResult>
{
    public async Task<PublishSensorsDataCommandResult> Handle(PublishSensorsDataCommand request, CancellationToken cancellationToken)
    {
        var events = request.SensorsData.Select(x => 
            new SensorDataArrivedEvent(
                x.DataType,
                x.PlacementName,
                x.Timestamp,
                x.Payload
            )
        );

        await eventPublisher.PublishBatchAsync(events, cancellationToken);

        return new PublishSensorsDataCommandResult();
    }
}
