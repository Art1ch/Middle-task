using Shared.Abstractions.Events.Abstract;
using System.Text.Json;

namespace Shared.Abstractions.Events;

public sealed record SensorDataArrivedEvent(
    string DataType,
    string PlacementName,
    DateTime Timestamp,
    JsonElement Payload
) : EventBase, IEvent;