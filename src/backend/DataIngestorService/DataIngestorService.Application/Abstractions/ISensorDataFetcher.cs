using Shared.Abstractions.Models;

namespace DataIngestorService.Application.Abstractions;

public interface ISensorDataFetcher
{
    Task<IEnumerable<SensorDataItemModel>> FetchData(CancellationToken cancellationToken = default);
}
