using System;
using MonolithTemplate.Notifications.Domain.Emails;

namespace MonolithTemplate.Notifications.Application.EmailSender;

public interface IEmailSender
{
    Task SendAsync(EmailMessage message);
}
