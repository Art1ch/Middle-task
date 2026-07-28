using DataProcessorService.Application.Abstractions;
using DataProcessorService.Core.Entities;
using MassTransit;
using Microsoft.Extensions.Logging;
using Shared.Abstractions.Events;

namespace DataProcessorService.Application.Consumers;

internal sealed class SensorDataArrivedEventConsumer(
    ISensorDataRepository sensorDataRepository,
    ILogger<SensorDataArrivedEventConsumer> logger
) : IConsumer<Batch<SensorDataArrivedEvent>>
{
    public async Task Consume(ConsumeContext<Batch<SensorDataArrivedEvent>> context)
    {
        var entities = context.Message.Select(x => new SensorDataEntity
        {
            DataType = x.Message.DataType,
            PlacementName = x.Message.PlacementName,
            Timestamp = x.Message.Timestamp,
            Payload = x.Message.Payload,
        });

        await sensorDataRepository.CreateRangeAsync(entities);

        logger.LogInformation("Sensors data added");
    }
}
