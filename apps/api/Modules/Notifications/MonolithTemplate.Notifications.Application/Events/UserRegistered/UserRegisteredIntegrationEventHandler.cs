using MonolithTemplate.Identity.Contracts;
using MonolithTemplate.Notifications.Application.Services;
using MonolithTemplate.Notifications.Domain.Emails;
using MonolithTemplate.Notifications.Domain.ValueObjects;
using MonolithTemplate.Shared.Events;

namespace MonolithTemplate.Notifications.Application.Events.UserRegistered;

public class UserRegisteredIntegrationEventHandler : IIntegrationEventHandler<UserRegisteredIntegrationEvent>
{
    private readonly IMailer _emailSender;
    private readonly IEmailTemplateRenderer _emailTemplateRenderer;

    public UserRegisteredIntegrationEventHandler(IMailer emailSender, IEmailTemplateRenderer emailTemplateRenderer)
    {
        _emailSender = emailSender;
        _emailTemplateRenderer = emailTemplateRenderer;
    }
    public async Task Handle(UserRegisteredIntegrationEvent @event, CancellationToken ct = default)
    {
        var messageKey = $"identity:confirm-email:{@event.Id}";

        try
        {
            var confirmationUrl =
                $"https://localhost:3000/confirm?userId={Uri.EscapeDataString(@event.Id.ToString())}&token={Uri.EscapeDataString(@event.ConfirmationToken)}";


            var email = Email.Create(@event.Email);
            if (!email.IsSuccess)
                throw new ApplicationException("Błąd przy tworzeniu email");

            var subject = EmailSubject.Create("Potwierdź rejestrację w VisitMe");
            if (!email.IsSuccess)
                throw new ApplicationException("Błąd przy tworzeniu email");

            var html = await _emailTemplateRenderer.RenderAsync(
                    "confirm-email",
                    new Dictionary<string, string>
                    {
                        ["UserName"] = "Użytkowniku",
                        ["ConfirmationUrl"] = confirmationUrl
                    },
                ct);

            var htmlBody = EmailHtmlBody.Create(html);

            if (!htmlBody.IsSuccess)
                throw new ApplicationException("Błąd przy tworzeniu email");

            var message = new EmailMessage(
                [email.Value],
                subject.Value,
                htmlBody.Value);

            await _emailSender.SendAsync(message);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Błąd podczas wysyłki maila rejestracyjnego.", ex);

        }


    }

}
