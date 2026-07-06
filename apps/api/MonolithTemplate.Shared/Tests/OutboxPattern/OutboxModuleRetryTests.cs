using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using MonolithTemplate.Shared.Events;
using MonolithTemplate.Shared.OutboxPattern;
using Xunit;

namespace MonolithTemplate.Shared.Tests.OutboxPattern;

public sealed class OutboxModuleRetryTests
{
    [Fact]
    public async Task ProcessAsync_WhenEventHandlerThrows_ShouldIncrementAttemptAndScheduleRetry()
    {
        var options = new DbContextOptionsBuilder<TestOutboxDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        await using var db = new TestOutboxDbContext(options);
        var @event = new RetryEvent("payload");

        var message = new OutboxMessage
        {
            OccurredOnUtc = DateTime.UtcNow,
            Type = typeof(RetryEvent).AssemblyQualifiedName!,
            Payload = JsonSerializer.Serialize(@event)
        };

        db.Set<OutboxMessage>().Add(message);
        await db.SaveChangesAsync();

        var module = new OutboxModule<TestOutboxDbContext>(db, new ThrowingEventBus());

        var processed = await module.ProcessAsync(CancellationToken.None);

        Assert.Equal(1, processed);

        var updated = await db.Set<OutboxMessage>().SingleAsync();
        Assert.Equal(1, updated.AttemptCount);
        Assert.Null(updated.ProcessedOnUtc);
        Assert.NotNull(updated.NextTryOnUtc);
        Assert.NotNull(updated.Error);
    }

    private sealed class TestOutboxDbContext(DbContextOptions<TestOutboxDbContext> options) : DbContext(options)
    {
        public DbSet<OutboxMessage> OutboxMessages => Set<OutboxMessage>();
    }

    private sealed record RetryEvent(string Value) : IIntegrationEvent;

    private sealed class ThrowingEventBus : IEventBus
    {
        public Task Publish<TEvent>(TEvent @event, CancellationToken ct = default) where TEvent : IIntegrationEvent
            => throw new InvalidOperationException("Simulated event bus failure");
    }
}
