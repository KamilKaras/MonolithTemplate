using Microsoft.AspNetCore.Identity;
using MonolithTemplate.Identity.Contracts;
using MonolithTemplate.Notifications.Application.Services;
using MonolithTemplate.Shared.Events;

namespace MonolithTemplate.Notifications.Application.Events.UserRegistered;

public class UserRegisteredIntegrationEventHandler : IIntegrationEventHandler<UserRegisteredIntegrationEvent>
{
    private readonly IMailer _emailSender;

    public UserRegisteredIntegrationEventHandler(IMailer emailSender)
    {
        _emailSender = emailSender;

    }
    public async Task Handle(UserRegisteredIntegrationEvent @event, CancellationToken ct = default)
    {
        var messageKey = $"identity:confirm-email:{@event.Id}";

        try
        {
            var confirmationUrl =
                $"https://twojfrontend/confirm?userId={@event.Id}&token={@event.ConfirmationToken}";

            var body = $"""
                Kliknij aby potwierdzić konto:
                {confirmationUrl}
                """;
            var result = Email.Create(@event.Email);

            var message = new EmailMessage(
                [result.Value],
                "Potwierdzenie logowania VisitMe",
                body,
                "");

            await _emailSender.SendAsync(message);
        }
        catch
        {
            throw new NotImplementedException();

        }


    }

}
