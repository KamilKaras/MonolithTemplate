using System;
using MonolithTemplate.Notifications.Domain.Emails;

namespace MonolithTemplate.Notifications.Application.Services;

public interface IMailer
{
    Task SendAsync(EmailMessage message);
}
