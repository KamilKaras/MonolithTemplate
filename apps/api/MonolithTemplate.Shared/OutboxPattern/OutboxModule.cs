using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using MonolithTemplate.Shared.Events;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MonolithTemplate.Shared.OutboxPattern;

public sealed class OutboxModule<TDbContext> : IOutboxModule
    where TDbContext : DbContext
{
    private readonly TDbContext _db;
    private readonly IEventBus _eventBus;

    public OutboxModule(TDbContext db, IEventBus eventBus)
    {
        _db = db;
        _eventBus = eventBus;
    }
    public async Task<int> ProcessAsync(CancellationToken ct)
    {
        var messages = await _db.Set<OutboxMessage>()
            .Where(w => w.ProcessedOnUtc == null
                && !w.Poisoned
                && w.AttemptCount < 4
                && (w.NextTryOnUtc == null || w.NextTryOnUtc <= DateTime.UtcNow))
            .OrderBy(o => o.OccurredOnUtc)
            .Take(50)
            .ToListAsync();

        foreach (var message in messages)
        {
            try
            {
                var type = Type.GetType(message.Type, throwOnError: true);
                var evt = JsonSerializer.Deserialize(message.Payload, type!);

                await _eventBus.Publish((dynamic)evt!, ct);

                message.ProcessedOnUtc = DateTime.UtcNow;
                message.AttemptCount++;
                message.NextTryOnUtc = null;
                message.Error = null;
            }
            catch (Exception ex)
            {
                message.AttemptCount++;
                message.Error = ex.ToString();

                var delayMinutes = Math.Min(10, message.AttemptCount * 2);
                message.NextTryOnUtc = DateTime.UtcNow.AddMinutes(delayMinutes);
            }
        }
        var messagesCount = messages.Count;

        if (messagesCount > 0)
            await _db.SaveChangesAsync();

        return messagesCount;
    }
}
