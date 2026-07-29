using DataProcessorService.Application.Abstractions;
using DataProcessorService.Application.Filters;
using MapsterMapper;
using MediatR;
using Shared.Abstractions.Models;

namespace DataProcessorService.Application.Queries.GetSensorsDataByFilterQuery;

internal sealed class GetSensorsDataByFilterQueryHandler(
    ISensorDataRepository sensorDataRepository,
    IMapper mapper
) : IRequestHandler<GetSensorsDataByFilterQuery, GetSensorsDataByFilterQueryResult>
{
    public async Task<GetSensorsDataByFilterQueryResult> Handle(GetSensorsDataByFilterQuery request, CancellationToken cancellationToken)
    {
        var filter = new SensorDataFilter(
            request.Type,
            request.PlacementName,
            request.Page,
            request.PageSize,
            request.From,
            request.To
        );

        var entities = await sensorDataRepository.GetByFilter(filter, cancellationToken);

        var items = mapper.Map<List<SensorsDataItemModel>>(entities);

        return new GetSensorsDataByFilterQueryResult(items);
    }
}
