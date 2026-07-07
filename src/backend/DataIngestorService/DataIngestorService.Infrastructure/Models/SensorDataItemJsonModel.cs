using System.Text.Json;
using System.Text.Json.Serialization;

namespace DataIngestorService.Infrastructure.Models;

internal sealed class SensorDataItemJsonModel
{
    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("payload")]
    public JsonElement Payload { get; set; }
}
