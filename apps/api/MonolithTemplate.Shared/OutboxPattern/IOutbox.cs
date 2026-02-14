using MonolithTemplate.Shared.Events;

namespace MonolithTemplate.Shared.OutboxPattern;

public interface IOutbox
{
    void Enqueue<T>(T @event)
    where T : notnull, IIntegrationEvent;
    IReadOnlyCollection<IIntegrationEvent> DequeueAll();
}