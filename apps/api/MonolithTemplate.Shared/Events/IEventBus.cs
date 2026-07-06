namespace MonolithTemplate.Shared.Events;

public interface IEventBus
{
    /// <summary>
    /// Publishes an integration event to in-process handlers.
    /// Reserve this for explicitly synchronous flows or for dispatching events
    /// that have already been persisted through the outbox.
    /// </summary>
    Task Publish<TEvent>(TEvent @event, CancellationToken ct = default) where TEvent : IIntegrationEvent;
}
