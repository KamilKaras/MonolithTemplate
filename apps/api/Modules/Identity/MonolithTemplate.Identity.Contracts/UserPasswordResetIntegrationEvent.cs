using MonolithTemplate.Shared.Events;

namespace MonolithTemplate.Identity.Contracts;

public sealed record UserPasswordResetIntegrationEvent : IIntegrationEvent {
    public UserPasswordResetIntegrationEvent(Guid id, string email, string resetToken) {
        Id = id;
        Email = email;
        ResetToken = resetToken;
    }

    public Guid Id {
        get;
    }
    public string Email {
        get;
    }
    public string ResetToken {
        get;
    }
}
