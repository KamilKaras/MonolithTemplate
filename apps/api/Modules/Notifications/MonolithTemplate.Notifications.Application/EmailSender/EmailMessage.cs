using System;

namespace MonolithTemplate.Notifications.Application.EmailSender;

public sealed class EmailMessage
{
    public EmailMessage(
        Email[] to,
        string subject,
        string htmlBody,
        string textBody,
        Email[]? bcc = null,
        Email[]? cc = null

    )
    {
        To = to;
        Subject = subject;
        HtmlBody = htmlBody;
        TextBody = textBody;
        Bcc = bcc;
        Cc = cc;
    }

    public Email[] To { get; }
    public string Subject { get; }
    public string HtmlBody { get; }
    public string TextBody { get; }
    public Email[]? Bcc { get; }
    public Email[]? Cc { get; }
}
