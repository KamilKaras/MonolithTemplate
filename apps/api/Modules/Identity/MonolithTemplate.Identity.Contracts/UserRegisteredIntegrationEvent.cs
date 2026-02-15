using MonolithTemplate.Shared.Events;

namespace MonolithTemplate.Identity.Contracts;

public sealed class UserRegisteredIntegrationEvent : IIntegrationEvent
{
    public UserRegisteredIntegrationEvent(Guid id)
    {
        Id = id;

    }

    public Guid Id { get; }
}
