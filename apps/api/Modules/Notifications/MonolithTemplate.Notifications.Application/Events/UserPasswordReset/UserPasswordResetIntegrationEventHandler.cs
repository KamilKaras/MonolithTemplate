using MonolithTemplate.Identity.Contracts;
using MonolithTemplate.Notifications.Application.Services;
using MonolithTemplate.Notifications.Domain.Emails;
using MonolithTemplate.Notifications.Domain.ValueObjects;
using MonolithTemplate.Shared.Events;

namespace MonolithTemplate.Notifications.Application.Events.UserRegistered;

public class UserPasswordResetIntegrationEventHandler : IIntegrationEventHandler<UserPasswordResetIntegrationEvent> {
    private readonly IMailer _emailSender;
    private readonly IEmailTemplateRenderer _emailTemplateRenderer;

    public UserPasswordResetIntegrationEventHandler(IMailer emailSender, IEmailTemplateRenderer emailTemplateRenderer) {
        _emailSender = emailSender;
        _emailTemplateRenderer = emailTemplateRenderer;
    }
    public async Task Handle(UserPasswordResetIntegrationEvent @event, CancellationToken ct = default) {
        var messageKey = $"identity:confirm-email:{@event.Id}";

        try {
            var resetUrl =
                $"http://localhost:3000/reset-password?userId={Uri.EscapeDataString(@event.Id.ToString())}&token={Uri.EscapeDataString(@event.ResetToken)}";

            var email = Email.Create(@event.Email);

            if (!email.IsSuccess)
                throw new ApplicationException("Błąd przy tworzeniu wiadomości email");

            var subject = EmailSubject.Create("Reset hasła w VisitMe");

            if (!email.IsSuccess)
                throw new ApplicationException("Błąd przy tworzeniu wiadomości email");

            var html = await _emailTemplateRenderer.RenderAsync(
                    "reset-password",
                    new Dictionary<string, string> {
                        ["UserName"] = "Użytkowniku",
                        ["ResetUrl"] = resetUrl
                    },
                ct);

            var htmlBody = EmailHtmlBody.Create(html);

            if (!htmlBody.IsSuccess)
                throw new ApplicationException("Błąd przy tworzeniu wiadomości email");

            var message = new EmailMessage(
                [email.Value],
                subject.Value,
                htmlBody.Value);

            await _emailSender.SendAsync(message);
        }
        catch (Exception ex) {
            throw new ApplicationException("Błąd podczas wysyłki maila rejestracyjnego.", ex);

        }


    }

}
