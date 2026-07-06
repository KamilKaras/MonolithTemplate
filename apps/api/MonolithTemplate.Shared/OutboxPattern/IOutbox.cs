using MonolithTemplate.Shared.Events;

namespace MonolithTemplate.Shared.OutboxPattern;

public interface IOutbox
{
    /// <summary>
    /// Captures integration events during command handling so they can be
    /// persisted and dispatched after the unit of work commits.
    /// </summary>
    void Enqueue<T>(T @event)
        where T : notnull, IIntegrationEvent;

    IReadOnlyCollection<IIntegrationEvent> DequeueAll();
}
