using DataProcessorService.Application.Filters;
using DataProcessorService.Core.Entities;
using Shared.Abstractions.Repository;
using Shared.Abstractions.Repository.Abstract;

namespace DataProcessorService.Application.Abstractions;

internal interface ISensorDataRepository : IRepository<EntityBase>
{
    Task<IEnumerable<SensorDataEntity>> GetByFilter(SensorDataFilter dataFilter, CancellationToken cancellationToken = default);
}
