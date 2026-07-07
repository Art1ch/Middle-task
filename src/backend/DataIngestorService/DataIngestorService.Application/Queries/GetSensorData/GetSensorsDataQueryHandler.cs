using DataIngestorService.Application.Abstractions;
using MediatR;

namespace DataIngestorService.Application.Queries.GetSensorData;

internal sealed class GetSensorsDataQueryHandler(
    ISensorDataFetcher sensorDataFetcher
) : IRequestHandler<GetSensorsDataQuery, GetSensorsDataQueryResult>
{
    public async Task<GetSensorsDataQueryResult> Handle(GetSensorsDataQuery request, CancellationToken cancellationToken)
    {
        var sensorData = await sensorDataFetcher.FetchData(cancellationToken);

        return new GetSensorsDataQueryResult(sensorData);
    }
}
