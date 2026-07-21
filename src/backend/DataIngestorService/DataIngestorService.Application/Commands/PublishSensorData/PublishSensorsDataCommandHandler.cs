using MassTransit;
using MediatR;
using Shared.Abstractions.Events;

namespace DataIngestorService.Application.Commands.PublishSensorData;

internal sealed class PublishSensorsDataCommandHandler(
    IPublishEndpoint publishEndpoint
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

        await publishEndpoint.PublishBatch(events, cancellationToken);

        return new PublishSensorsDataCommandResult();
    }
}
