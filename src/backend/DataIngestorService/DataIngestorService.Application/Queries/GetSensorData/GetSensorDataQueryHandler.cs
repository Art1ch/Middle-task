using DataIngestorService.Application.Abstractions;
using MediatR;

namespace DataIngestorService.Application.Queries.GetSensorData;

internal sealed class GetSensorDataQueryHandler(
    ISensorDataFetcher sensorDataFetcher
) : IRequestHandler<GetSensorDataQuery, GetSensorDataQueryResult>
{
    public async Task<GetSensorDataQueryResult> Handle(GetSensorDataQuery request, CancellationToken cancellationToken)
    {
        var sensorData = await sensorDataFetcher.FetchData(cancellationToken);

        return new GetSensorDataQueryResult(sensorData);
    }
}
