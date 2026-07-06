using Microsoft.Extensions.Options;
using MonolithTemplate.Notifications.Application.Services;
using MonolithTemplate.Notifications.Domain.Emails;
using Microsoft.Extensions.Logging;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;

namespace MonolithTemplate.Notifications.Infrastructure.Services;

public sealed class Mailer : IMailer
{
    private readonly SmtpSettings _smtpSettings;
    private readonly ILogger<Mailer> _logger;


    public Mailer(IOptions<SmtpSettings> smtpSettings, ILogger<Mailer> logger)
    {
        _smtpSettings = smtpSettings.Value;
        _logger = logger;

    }
    public async Task SendAsync(EmailMessage message)
    {
        try
        {
            var mime = new MimeMessage();

            mime.From.Add(new MailboxAddress(_smtpSettings.SenderName, _smtpSettings.SenderEmail));
            AddRecipients(message, mime);

            mime.Subject = message.Subject.Value;

            var builder = new BodyBuilder
            {
                HtmlBody = message.HtmlBody.Value,
                TextBody = message.TextBody?.Value
            };

            mime.Body = builder.ToMessageBody();

            using var client = new SmtpClient();

            var secure = ParseSecurity(_smtpSettings.Security);

            await client.ConnectAsync(_smtpSettings.Server, _smtpSettings.Port, secure);

            if (!string.IsNullOrWhiteSpace(_smtpSettings.UserName))
                await client.AuthenticateAsync(_smtpSettings.UserName, _smtpSettings.Password);

            await client.SendAsync(mime);
            await client.DisconnectAsync(true);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Wystąpił błąd podczas wysyłki maila do: {Recipients}, tytuł: {Subject}",
                string.Join(',', message.To.Select(s => s.Value)),
                message.Subject.Value);
            throw;
        }

    }

    private static void AddRecipients(EmailMessage message, MimeMessage messageToSend)
    {
        foreach (var to in message.To)
            messageToSend.To.Add(new MailboxAddress(null, to.Value));
        foreach (var cc in message.Cc ?? [])
            messageToSend.Cc.Add(new MailboxAddress(null, cc.Value));
        foreach (var bcc in message.Bcc ?? [])
            messageToSend.Bcc.Add(new MailboxAddress(null, bcc.Value));
    }

    public static SecureSocketOptions ParseSecurity(string? security)
    {
        if (string.IsNullOrWhiteSpace(security))
        {
            return SecureSocketOptions.None;
        }

        return security.Trim().ToUpperInvariant() switch
        {
            "NONE" => SecureSocketOptions.None,
            "SSL" or "SSLONCONNECT" => SecureSocketOptions.SslOnConnect,
            "STARTTLS" => SecureSocketOptions.StartTls,
            "STARTTLSWHENAVAILABLE" => SecureSocketOptions.StartTlsWhenAvailable,
            _ => throw new InvalidOperationException($"Unsupported SMTP security option: {security}")
        };
    }
}
