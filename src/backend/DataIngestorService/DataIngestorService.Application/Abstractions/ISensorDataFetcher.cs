using Shared.Abstractions.Models;

namespace DataIngestorService.Application.Abstractions;

public interface ISensorDataFetcher
{
    Task<IEnumerable<SensorsDataItemModel>> FetchData(CancellationToken cancellationToken = default);
}
