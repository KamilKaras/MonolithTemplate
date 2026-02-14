namespace MonolithTemplate.Shared.Events;

public interface IEventBus
{
    Task Publish<TEvent>(TEvent @event, CancellationToken ct = default) where TEvent : IIntegrationEvent;
}
