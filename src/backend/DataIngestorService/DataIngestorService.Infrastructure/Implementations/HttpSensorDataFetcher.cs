using DataIngestorService.Application.Abstractions;
using DataIngestorService.Infrastructure.Models;
using Shared.Abstractions.Models;
using System.Net.Http.Json;

namespace DataIngestorService.Infrastructure.Implementations;

internal sealed class HttpSensorDataFetcher : ISensorDataFetcher
{
    private readonly HttpClient _httpClient;

    public HttpSensorDataFetcher(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<SensorDataItemModel>> FetchData(CancellationToken cancellationToken = default)
    {
        var json = await _httpClient.GetAsync("meters", cancellationToken);
        // Add validation of error code

        var response = json.Content.ReadFromJsonAsync<List<SensorDataItemJsonModel>>(cancellationToken);

        // Add parsing of list of json models into the list of normal models

        throw new NotImplementedException();
    }
}