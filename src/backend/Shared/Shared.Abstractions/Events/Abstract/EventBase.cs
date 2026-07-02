namespace Shared.Abstractions.Events.Abstract;

public abstract record EventBase
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime EventTimestamp { get; set; } = DateTime.UtcNow;
}
