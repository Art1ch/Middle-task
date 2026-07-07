using DataIngestorService.Application.Abstractions;
using DataIngestorService.Infrastructure.Models;
using Shared.Abstractions.Models;
using System.Text.Json;

namespace DataIngestorService.Infrastructure.Implementations;

internal sealed class HttpSensorDataFetcher : ISensorDataFetcher
{
    private readonly HttpClient _httpClient;

    public HttpSensorDataFetcher(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<SensorsDataItemModel>> FetchData(CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetAsync("meters", cancellationToken);
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync(cancellationToken);
        try
        {
            var items = JsonSerializer.Deserialize<List<SensorDataItemJsonModel>>(content);

            var result = items.Select(x =>
                new SensorsDataItemModel {
                    DataType = x.Type,
                    PlacementName = x.Name,
                    Payload = x.Payload,
                    Timestamp = DateTime.UtcNow
                }
            );

            return result;
        }
        catch (JsonException)
        {
            throw new InvalidOperationException("External API error");
        }
    }

}