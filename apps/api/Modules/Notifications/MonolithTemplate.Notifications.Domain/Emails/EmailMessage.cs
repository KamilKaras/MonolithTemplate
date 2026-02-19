using System;
using MonolithTemplate.Notifications.Domain.ValueObjects;
using MonolithTemplate.Shared.ResultPattern;

namespace MonolithTemplate.Notifications.Domain.Emails;

public sealed record EmailMessage
{
    public EmailMessage(
        Email[] to,
        EmailSubject subject,
        EmailHtmlBody htmlBody,
        EmailTextBody? textBody = null,
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
    public EmailSubject Subject { get; }
    public EmailHtmlBody HtmlBody { get; }
    public EmailTextBody? TextBody { get; }
    public Email[]? Bcc { get; }
    public Email[]? Cc { get; }

}
