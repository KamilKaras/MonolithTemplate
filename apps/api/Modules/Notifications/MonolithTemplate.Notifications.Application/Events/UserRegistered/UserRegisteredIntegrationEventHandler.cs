using MonolithTemplate.Identity.Contracts;
using MonolithTemplate.Notifications.Application.Services;
using MonolithTemplate.Notifications.Domain.Emails;
using MonolithTemplate.Notifications.Domain.ValueObjects;
using MonolithTemplate.Shared.Events;
using MonolithTemplate.Shared.ValueObjects;

namespace MonolithTemplate.Notifications.Application.Events.UserRegistered;

public class UserRegisteredIntegrationEventHandler : IIntegrationEventHandler<UserRegisteredIntegrationEvent>
{
    private readonly IMailer _emailSender;
    private readonly IEmailTemplateRenderer _emailTemplateRenderer;
    private readonly IFrontendUrlProvider _frontendUrlProvider;

    public UserRegisteredIntegrationEventHandler(
        IMailer emailSender,
        IEmailTemplateRenderer emailTemplateRenderer,
        IFrontendUrlProvider frontendUrlProvider)
    {
        _emailSender = emailSender;
        _emailTemplateRenderer = emailTemplateRenderer;
        _frontendUrlProvider = frontendUrlProvider;
    }

    public async Task Handle(UserRegisteredIntegrationEvent @event, CancellationToken ct = default)
    {
        try
        {
            var confirmationUrl = _frontendUrlProvider.BuildConfirmationUrl(@event.Id, @event.ConfirmationToken);

            var email = Email.Create(@event.Email);
            if (!email.IsSuccess)
                throw new ApplicationException("Błąd przy tworzeniu email");

            var subject = EmailSubject.Create("Potwierdź rejestrację w VisitMe");
            if (!subject.IsSuccess)
                throw new ApplicationException("Błąd przy tworzeniu tytułu email");

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
