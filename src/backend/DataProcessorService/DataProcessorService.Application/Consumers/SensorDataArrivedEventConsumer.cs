using DataProcessorService.Application.Abstractions;
using DataProcessorService.Core.Entities;
using MapsterMapper;
using MassTransit;
using Microsoft.Extensions.Logging;
using Shared.Abstractions.Events;

namespace DataProcessorService.Application.Consumers;

internal sealed class SensorDataArrivedEventConsumer(
    ISensorDataRepository sensorDataRepository,
    IMapper mapper,
    ILogger<SensorDataArrivedEventConsumer> logger
) : IConsumer<SensorDataArrivedEvent>
{
    public async Task Consume(ConsumeContext<SensorDataArrivedEvent> context)
    {
        var entity = mapper.Map<SensorDataEntity>(context.Message);

        await sensorDataRepository.CreateAsync(entity);

        logger.LogInformation($"Sensor data added {DateTime.UtcNow}");
    }
}
