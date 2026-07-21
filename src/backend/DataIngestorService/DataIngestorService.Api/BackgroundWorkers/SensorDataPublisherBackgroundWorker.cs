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

    public SensorDataPublisherBackgroundWorker(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected async override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
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

        var sender = scope.ServiceProvider.GetRequiredService<ISender>();

        var query = new GetSensorsDataQuery();
        var queryResult = await sender!.Send(query, stoppingToken);

        var command = new PublishSensorsDataCommand(queryResult.SensorsData);
        await sender!.Send(command);
    }
}