using DataProcessorService.Application.Abstractions;
using DataProcessorService.Application.Filters;
using DataProcessorService.Core.Entities;
using DataProcessorService.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Shared.Implementations.AbstractRepository;

namespace DataProcessorService.Infrastructure.Repository;

internal sealed class SensorDataRepository : RepositoryBase<SensorDataEntity>, ISensorDataRepository
{
    private readonly SensorsDataContext _sensorsDataContext;

    public SensorDataRepository(SensorsDataContext context) : base(context)
    {
        _sensorsDataContext = context;
    }

    public async Task<IEnumerable<SensorDataEntity>> GetByFilter(SensorDataFilter dataFilter, CancellationToken cancellationToken = default)
    {
        var query = _sensorsDataContext.Sensors.AsNoTracking();

        if (string.IsNullOrEmpty(dataFilter.Type))
            query = query.Where(x => x.DataType == dataFilter.Type);

        if (string.IsNullOrEmpty(dataFilter.PlacementName))
            query = query.Where(x => x.PlacementName == dataFilter.PlacementName);

        if (dataFilter.From != null)
            query = query.Where(x => x.Timestamp >= dataFilter.From);

        if (dataFilter.To != null)
            query = query.Where(x => x.Timestamp <= dataFilter.To);

        var entities = await query
            .OrderBy(x => x.Timestamp)
            .Skip(dataFilter.Page - 1)
            .Take(dataFilter.PageSize)
            .ToListAsync();

        return entities;
    }
}