using MonolithTemplate.Shared.Events;
using MonolithTemplate.Shared.OutboxPattern;

public sealed class Outbox : IOutbox
{
    private readonly List<IntegrationEvent> _events = new();

    public void Enqueue<T>(T @event) where T : notnull, IntegrationEvent => _events.Add(@event);

    public IReadOnlyCollection<IntegrationEvent> DequeueAll()
    {
        var copy = _events.ToArray();
        _events.Clear();
        return copy;
    }
}
