using Microsoft.AspNetCore.Identity;
using MonolithTemplate.Identity.Contracts;
using MonolithTemplate.Notifications.Application.Services;
using MonolithTemplate.Notifications.Domain.Emails;
using MonolithTemplate.Notifications.Domain.ValueObjects;
using MonolithTemplate.Shared.Events;
using MonolithTemplate.Shared.ResultPattern;

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


            var email = Email.Create(@event.Email);
            if (!email.IsSuccess)
                throw new ApplicationException("Błąd przy tworzeniu email");

            var subject = EmailSubject.Create("Potwierdź rejestrację w VisitMe");
            if (!email.IsSuccess)
                throw new ApplicationException("Błąd przy tworzeniu email");

            var htmlBody = EmailHtmlBody.Create($"""
                Kliknij aby potwierdzić konto:
                {confirmationUrl}
                """);
            if (!htmlBody.IsSuccess)
                throw new ApplicationException("Błąd przy tworzeniu email");

            var message = new EmailMessage(
                [email.Value],
                subject.Value,
                htmlBody.Value);

            await _emailSender.SendAsync(message);
        }
        catch
        {
            throw new NotImplementedException();

        }


    }

}
