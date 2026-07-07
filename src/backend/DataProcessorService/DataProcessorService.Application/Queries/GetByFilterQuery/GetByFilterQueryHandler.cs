using DataProcessorService.Application.Abstractions;
using DataProcessorService.Application.Filters;
using MediatR;
using Shared.Abstractions.Models;

namespace DataProcessorService.Application.Queries.GetByFilterQuery;

internal sealed class GetByFilterQueryHandler(
    ISensorDataRepository sensorDataRepository
) : IRequestHandler<GetByFilterQuery, GetByFilterQueryResult>
{
    public async Task<GetByFilterQueryResult> Handle(GetByFilterQuery request, CancellationToken cancellationToken)
    {
        var filter = new SensorDataFilter(
            request.Type,
            request.PlacementName,
            request.From,
            request.To,
            request.Page,
            request.PageSize
        );

        var entities = await sensorDataRepository.GetByFilter(filter, cancellationToken);

        var items = entities.Select(x => new SensorsDataItemModel
        {
            DataType = x.DataType,
            PlacementName = x.PlacementName,
            Payload = x.Payload,
            Timestamp = x.Timestamp
        });

        return new GetByFilterQueryResult(items);
    }
}
