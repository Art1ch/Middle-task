using Shared.Abstractions.Events.Abstract;

namespace Shared.Abstractions.Messaging;

public interface IEventPublisher<TEvent> where TEvent : IEvent
{
    Task PublishAsync(TEvent @event, CancellationToken cancellationToken = default);
    Task PublishBatchAsync(IEnumerable<TEvent> events, CancellationToken cancellationToken = default);
}