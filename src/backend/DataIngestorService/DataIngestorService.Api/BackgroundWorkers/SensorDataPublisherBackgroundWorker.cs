using Cronos;
using DataIngestorService.Application.Commands.PublishSensorData;
using DataIngestorService.Application.Queries.GetSensorData;
using MediatR;

namespace DataIngestorService.Api.BackgroundWorkers;

public class SensorDataPublisherBackgroundWorker : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly TimeZoneInfo _timeZoneInfo = TimeZoneInfo.Utc;
    private readonly CronExpression _cron = CronExpression.Parse("*/20 * * * * *", CronFormat.IncludeSeconds);

    protected async override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        if (!stoppingToken.IsCancellationRequested)
        {
            var now = DateTimeOffset.UtcNow;
            var next = _cron.GetNextOccurrence(now, _timeZoneInfo);
            var delay = next!.Value - DateTimeOffset.UtcNow;

            if (delay > TimeSpan.Zero)
            {
                await Task.Delay(delay, stoppingToken);
            }

            await FetchAndPublishSensorData(stoppingToken);
        }
    }

    private async Task FetchAndPublishSensorData(CancellationToken stoppingToken)
    {
        using var scope = _serviceScopeFactory.CreateScope();

        var sender = scope.ServiceProvider.GetService<ISender>();

        var query = new GetSensorsDataQuery();
        var queryResult = await sender!.Send(query);

        var command = new PublishSensorsDataCommand(queryResult.SensorsData);
        await sender!.Send(command);
    }
}