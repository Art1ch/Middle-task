using DataProcessorService.Application.Filters;
using DataProcessorService.Core.Entities;
using Shared.Abstractions.Repository;

namespace DataProcessorService.Application.Abstractions;

public interface ISensorDataRepository : IRepository<SensorDataEntity>
{
    Task<IEnumerable<SensorDataEntity>> GetByFilter(SensorDataFilter dataFilter, CancellationToken cancellationToken = default);
}
