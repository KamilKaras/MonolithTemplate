using MonolithTemplate.Shared.Events;

namespace MonolithTemplate.Identity.Contracts;

public sealed record UserRegisteredIntegrationEvent : IIntegrationEvent
{
    public UserRegisteredIntegrationEvent(Guid id, string email, string confirmationToken)
    {
        Id = id;
        Email = email;
        ConfirmationToken = confirmationToken;
    }

    public Guid Id { get; }
    public string Email { get; }
    public string ConfirmationToken { get; }
}
