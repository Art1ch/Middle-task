using MapsterMapper;
using MassTransit;
using MediatR;
using Shared.Abstractions.Events;

namespace DataIngestorService.Application.Commands.PublishSensorData;

internal sealed class PublishSensorsDataCommandHandler(
    ITopicProducer<SensorDataArrivedEvent> topicProducer,
    IMapper mapper
) : IRequestHandler<PublishSensorsDataCommand, PublishSensorsDataCommandResult>
{
    public async Task<PublishSensorsDataCommandResult> Handle(PublishSensorsDataCommand request, CancellationToken cancellationToken)
    {
        var events = mapper.Map<List<SensorDataArrivedEvent>>(request.SensorsData);

        foreach (var @event in events)
             await topicProducer.Produce(@event);

        return new PublishSensorsDataCommandResult();
    }
}
