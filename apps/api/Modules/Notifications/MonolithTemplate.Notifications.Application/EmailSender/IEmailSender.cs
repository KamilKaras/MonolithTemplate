using System;

namespace MonolithTemplate.Notifications.Application.EmailSender;

public interface IEmailSender
{
    Task SendAsync(EmailMessage message);
}
