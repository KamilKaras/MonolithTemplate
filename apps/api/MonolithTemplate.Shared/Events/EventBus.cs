using Microsoft.Extensions.DependencyInjection;

namespace MonolithTemplate.Shared.Events;

public sealed class EventBus : IEventBus
{
    private readonly IServiceProvider _sp;

    public EventBus(IServiceProvider sp)
    {
        _sp = sp;
    }
    public async Task Publish<TEvent>(TEvent @event, CancellationToken ct = default) where TEvent : IIntegrationEvent
    {
        var handlers = _sp.GetServices<IIntegrationEventHandler<TEvent>>().ToArray();

        foreach (var handler in handlers)
            await handler.Handle(@event, ct);
    }
}
