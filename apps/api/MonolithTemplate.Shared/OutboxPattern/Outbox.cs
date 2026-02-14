using MonolithTemplate.Shared.Events;
using MonolithTemplate.Shared.OutboxPattern;

public sealed class Outbox : IOutbox
{
    private readonly List<IIntegrationEvent> _events = new();

    public void Enqueue<T>(T @event) where T : notnull, IIntegrationEvent => _events.Add(@event);

    public IReadOnlyCollection<IIntegrationEvent> DequeueAll()
    {
        var copy = _events.ToArray();
        _events.Clear();
        return copy;
    }
}
