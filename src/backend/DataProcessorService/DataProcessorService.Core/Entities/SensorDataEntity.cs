using Shared.Abstractions.Repository.Abstract;
using System.Text.Json;

namespace DataProcessorService.Core.Entities;

public sealed class SensorDataEntity : EntityBase
{
    public string DataType { get; set; }
    public string PlacementName { get; set; }
    public DateTime Timestamp { get; set; }
    public JsonElement Payload { get; set; }
}
