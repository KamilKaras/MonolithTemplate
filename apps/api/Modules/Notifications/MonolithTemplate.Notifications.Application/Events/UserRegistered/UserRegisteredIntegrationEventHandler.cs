using MonolithTemplate.Identity.Contracts;
using MonolithTemplate.Shared.Events;

namespace MonolithTemplate.Notifications.Application.Events.UserRegistered;

public class UserRegisteredIntegrationEventHandler : IIntegrationEventHandler<UserRegisteredIntegrationEvent>
{
    public Task Handle(UserRegisteredIntegrationEvent @event, CancellationToken ct = default)
    {
        var messageKey = $"identity:confirm-email:{@event.Id}";

        throw new NotImplementedException();
    }

}
