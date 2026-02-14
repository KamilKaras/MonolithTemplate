using System;

namespace MonolithTemplate.Shared.OutboxPattern;

public interface IOutboxModule
{
    Task<int> ProcessAsync(CancellationToken ct);
}
