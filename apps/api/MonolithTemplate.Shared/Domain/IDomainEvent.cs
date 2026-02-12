namespace MonolithTemplate.Shared.Domain;

public interface IDomainEvent
{
    DateTime OccurredOnUtc { get; }
}
