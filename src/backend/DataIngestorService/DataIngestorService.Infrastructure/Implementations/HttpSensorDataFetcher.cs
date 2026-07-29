using DataIngestorService.Application.Abstractions;
using DataIngestorService.Infrastructure.Models;
using MapsterMapper;
using Shared.Abstractions.Models;
using System.Text.Json;

namespace DataIngestorService.Infrastructure.Implementations;

internal sealed class HttpSensorDataFetcher : ISensorDataFetcher
{
    private readonly IMapper _mapper;
    private readonly HttpClient _httpClient;

    public HttpSensorDataFetcher(HttpClient httpClient, IMapper mapper)
    {
        _httpClient = httpClient;
        _mapper = mapper;
    }

    public async Task<IEnumerable<SensorsDataItemModel>> FetchData(CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetAsync("meters", cancellationToken);
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync(cancellationToken);
        try
        {
            var items = JsonSerializer.Deserialize<List<SensorDataItemJsonModel>>(content);

            var result = _mapper.Map<List<SensorsDataItemModel>>(items!);

            return result;
        }
        catch (JsonException)
        {
            throw new InvalidOperationException("External API error");
        }
    }

}