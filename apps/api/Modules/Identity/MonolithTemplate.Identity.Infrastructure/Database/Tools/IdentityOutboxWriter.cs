using MonolithTemplate.Identity.Infrastructure.Database;
using MonolithTemplate.Shared.OutboxPattern;
public interface IIdentityOutboxWriter : IOutboxWriter;

internal sealed class IdentityOutboxWriter(MyIdentityDbContext db) : IIdentityOutboxWriter
{
    public Task WriteAsync(IEnumerable<object> events, CancellationToken ct)
    {
        foreach (var evt in events)
        {
            db.OutboxMessages.Add(new OutboxMessage
            {
                Type = evt.GetType().AssemblyQualifiedName!,
                Payload = System.Text.Json.JsonSerializer.Serialize(evt, evt.GetType()),
                OccurredOnUtc = DateTime.UtcNow
            });
        }
        return Task.CompletedTask;
    }
}