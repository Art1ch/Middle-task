using DataProcessorService.Application.Abstractions;
using DataProcessorService.Core.Entities;
using MassTransit;
using Shared.Abstractions.Events;

namespace DataProcessorService.Application.Consumers;

internal sealed class SensorDataArrivedEventConsumer(
    ISensorDataRepository sensorDataRepository    
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
    }
}
