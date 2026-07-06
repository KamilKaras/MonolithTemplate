using Microsoft.Extensions.Logging.Abstractions;
using MonolithTemplate.Shared.Cqrs;
using MonolithTemplate.Shared.Database.UnitOfWork;
using MonolithTemplate.Shared.Events;
using MonolithTemplate.Shared.OutboxPattern;
using MonolithTemplate.Shared.ResultPattern;
using Xunit;

namespace MonolithTemplate.Shared.Tests.Database;

public sealed class UnitOfWorkBehaviorTests
{
    [Fact]
    public async Task CommandSuccess_ShouldBeginWriteOutboxAndCommit()
    {
        var uow = new FakeUnitOfWork();
        var outbox = new Outbox();
        var outboxWriter = new FakeOutboxWriter();
        var behavior = new UnitOfWorkBehavior<FakeCommand, Result, FakeUnitOfWork>(
            uow,
            outbox,
            outboxWriter,
            NullLogger<UnitOfWorkBehavior<FakeCommand, Result, FakeUnitOfWork>>.Instance);

        outbox.Enqueue(new FakeIntegrationEvent());

        var response = await behavior.Handle(
            new FakeCommand(),
            CancellationToken.None,
            () => Task.FromResult(Result.Success()));

        Assert.True(response.IsSuccess);
        Assert.Equal(1, uow.BeginCalls);
        Assert.Equal(1, uow.CommitCalls);
        Assert.Equal(0, uow.RollbackCalls);
        Assert.Equal(1, outboxWriter.WriteCalls);
    }

    [Fact]
    public async Task CommandFailureResult_ShouldRollbackAndNotCommit()
    {
        var uow = new FakeUnitOfWork();
        var outbox = new Outbox();
        var outboxWriter = new FakeOutboxWriter();
        var behavior = new UnitOfWorkBehavior<FakeCommand, Result, FakeUnitOfWork>(
            uow,
            outbox,
            outboxWriter,
            NullLogger<UnitOfWorkBehavior<FakeCommand, Result, FakeUnitOfWork>>.Instance);

        var response = await behavior.Handle(
            new FakeCommand(),
            CancellationToken.None,
            () => Task.FromResult(Result.Failure(Error.Validation("Test", "Validation"))));

        Assert.False(response.IsSuccess);
        Assert.Equal(1, uow.BeginCalls);
        Assert.Equal(0, uow.CommitCalls);
        Assert.Equal(1, uow.RollbackCalls);
        Assert.Equal(0, outboxWriter.WriteCalls);
    }

    private sealed class FakeCommand : ICommand<Result>
    {
    }

    private sealed class FakeUnitOfWork : IUnitOfWork
    {
        public int BeginCalls { get; private set; }
        public int CommitCalls { get; private set; }
        public int RollbackCalls { get; private set; }

        public Task Begin(CancellationToken ct)
        {
            BeginCalls++;
            return Task.CompletedTask;
        }

        public Task Commit(CancellationToken ct)
        {
            CommitCalls++;
            return Task.CompletedTask;
        }

        public Task Rollback(CancellationToken ct)
        {
            RollbackCalls++;
            return Task.CompletedTask;
        }
    }

    private sealed class FakeOutboxWriter : IOutboxWriter
    {
        public int WriteCalls { get; private set; }

        public Task WriteAsync(IEnumerable<IIntegrationEvent> events, CancellationToken ct)
        {
            WriteCalls++;
            return Task.CompletedTask;
        }
    }

    private sealed class FakeIntegrationEvent : IIntegrationEvent
    {
    }
}
