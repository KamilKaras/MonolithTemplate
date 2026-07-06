namespace MonolithTemplate.Shared.Events;

public interface IIntegrationEventHandler<in TEvent>
    where TEvent : IIntegrationEvent
{
    Task Handle(TEvent @event, CancellationToken ct = default);
}
