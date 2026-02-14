using MonolithTemplate.Shared.Events;

public interface IOutboxWriter
{
    Task WriteAsync(IEnumerable<IIntegrationEvent> events, CancellationToken ct);
}