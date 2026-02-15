using MonolithTemplate.Shared.Events;
using MonolithTemplate.Identity.Domain.IdentityModels;
using MonolithTemplate.Identity.Contracts;
using Microsoft.AspNetCore.Identity;

namespace MonolithTemplate.Identity.Application.Features.UserConfirmEmailSend;

public sealed class UserConfirmEmailSendHandler : IIntegrationEventHandler<UserRegisteredIntegrationEvent>
{
    private readonly UserManager<User> _userManager;

    public UserConfirmEmailSendHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }
    public async Task Handle(UserRegisteredIntegrationEvent @event, CancellationToken ct = default)
    {
        var user = await _userManager.FindByIdAsync(@event.Id.ToString());

    }
}
