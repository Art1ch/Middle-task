namespace Shared.Abstractions.Events.Abstract;

public interface IEvent
{
    public Guid Id { get; set; }
    public DateTime EventTimestamp { get; set; }
}
