using MonolithTemplate.Identity.Contracts;
using MonolithTemplate.Notifications.Application.Events.UserPasswordReset;
using MonolithTemplate.Notifications.Application.Events.UserRegistered;
using MonolithTemplate.Notifications.Application.Services;
using MonolithTemplate.Notifications.Domain.Emails;
using Xunit;

namespace MonolithTemplate.Notifications.Tests.Application;

public class NotificationHandlersTests
{
    [Fact]
    public async Task UserRegisteredHandler_ShouldUseGeneratedConfirmationUrl()
    {
        var mailer = new FakeMailer();
        var renderer = new FakeRenderer();
        var urls = new FakeFrontendUrls(
            confirmationUrl: "https://frontend.example/confirm?x=1",
            resetUrl: "https://frontend.example/reset-password?x=1");
        var handler = new UserRegisteredIntegrationEventHandler(mailer, renderer, urls);

        await handler.Handle(new UserRegisteredIntegrationEvent(Guid.NewGuid(), "user@example.com", "token-1"));

        Assert.Contains("https://frontend.example/confirm?x=1", renderer.LastVariables!["ConfirmationUrl"]);
        Assert.Single(mailer.Messages);
    }

    [Fact]
    public async Task UserPasswordResetHandler_ShouldUseGeneratedResetUrl()
    {
        var mailer = new FakeMailer();
        var renderer = new FakeRenderer();
        var urls = new FakeFrontendUrls(
            confirmationUrl: "https://frontend.example/confirm?x=1",
            resetUrl: "https://frontend.example/reset-password?x=1");
        var handler = new UserPasswordResetIntegrationEventHandler(mailer, renderer, urls);

        await handler.Handle(new UserPasswordResetIntegrationEvent(Guid.NewGuid(), "user@example.com", "token-2"));

        Assert.Contains("https://frontend.example/reset-password?x=1", renderer.LastVariables!["ResetUrl"]);
        Assert.Single(mailer.Messages);
    }

    private sealed class FakeMailer : IMailer
    {
        public List<EmailMessage> Messages { get; } = [];

        public Task SendAsync(EmailMessage message)
        {
            Messages.Add(message);
            return Task.CompletedTask;
        }
    }

    private sealed class FakeRenderer : IEmailTemplateRenderer
    {
        public Dictionary<string, string>? LastVariables { get; private set; }

        public Task<string> RenderAsync(string templateName, Dictionary<string, string> variables, CancellationToken ct = default)
        {
            LastVariables = variables;
            return Task.FromResult("<html>ok</html>");
        }
    }

    private sealed class FakeFrontendUrls(string confirmationUrl, string resetUrl) : IFrontendUrlProvider
    {
        public string BuildConfirmationUrl(Guid userId, string confirmationToken) => confirmationUrl;

        public string BuildResetPasswordUrl(Guid userId, string resetToken) => resetUrl;
    }
}
