using System.Text.Json;

namespace Shared.Abstractions.Models;

public sealed class SensorDataItemModel
{
    public string DataType { get; set; }
    public string PlacementName { get; set; }
    public DateTime Timestamp { get; set; }
    public JsonElement Payload { get; set; }
}