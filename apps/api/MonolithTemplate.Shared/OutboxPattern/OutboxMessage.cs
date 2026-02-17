namespace MonolithTemplate.Shared.OutboxPattern;

public sealed class OutboxMessage
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public DateTime OccurredOnUtc { get; init; }

    public string Type { get; init; } = default!;

    public string Payload { get; init; } = default!;

    public DateTime? ProcessedOnUtc { get; set; }
    public DateTime? NextTryOnUtc { get; set; }

    public int AttemptCount { get; set; }

    public string? Error { get; set; }
}