namespace MonolithTemplate.Notifications.Application.Services;

public interface IFrontendUrlProvider
{
    string BuildConfirmationUrl(Guid userId, string confirmationToken);

    string BuildResetPasswordUrl(Guid userId, string resetToken);
}
