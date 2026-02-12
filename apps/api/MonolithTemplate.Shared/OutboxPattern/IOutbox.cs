namespace MonolithTemplate.Shared.OutboxPattern;

public interface IOutbox
{
    void Enqueue<T>(T @event)
    where T : notnull, IIntegrationEvent;
    IReadOnlyCollection<object> DequeueAll();
}