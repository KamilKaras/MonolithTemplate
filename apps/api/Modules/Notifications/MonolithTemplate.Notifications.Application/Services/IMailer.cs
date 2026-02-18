using System;
using MonolithTemplate.Notifications.Domain.Emails;

namespace MonolithTemplate.Notifications.Application.EmailSender;

public interface IMailer
{
    Task SendAsync(EmailMessage message);
}
