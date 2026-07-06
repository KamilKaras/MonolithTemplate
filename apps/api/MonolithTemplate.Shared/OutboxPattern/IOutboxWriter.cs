using MonolithTemplate.Shared.Events;

namespace MonolithTemplate.Shared.OutboxPattern;

public interface IOutboxWriter
{
    Task WriteAsync(IEnumerable<IIntegrationEvent> events, CancellationToken ct);
}
