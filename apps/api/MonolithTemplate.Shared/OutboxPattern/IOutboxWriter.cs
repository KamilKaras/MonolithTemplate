public interface IOutboxWriter
{
    Task WriteAsync(IEnumerable<object> events, CancellationToken ct);
}