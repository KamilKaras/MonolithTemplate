using MonolithTemplate.Shared.Events;

public interface IOutboxWriter
{
    Task WriteAsync(IEnumerable<IntegrationEvent> events, CancellationToken ct);
}