using Shared.Abstractions.Events.Abstract;
using Shared.Abstractions.Models;

namespace Shared.Abstractions.Events;

public sealed record SensorDataArrivedEvent(
    IEnumerable<SensorDataItemModel> SensorDataItems
) : EventBase, IEvent;