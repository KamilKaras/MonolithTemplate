using MonolithTemplate.Shared.Messaging;
using MonolithTemplate.Shared.Results;

namespace MonolithTemplate.Identity.Application.Features.UserRegistration;

public sealed class UserRegistrationCommand : IRequest<Result<Guid>>
{
    public UserRegistrationCommand(
        string userName,
        string email,
        string password,
        string confirmPassword
    )
    {
        UserName = userName;
        Email = email;
        Password = password;
        ConfirmPassword = confirmPassword;
    }

    public string UserName { get; }
    public string Email { get; }
    public string Password { get; }
    public string ConfirmPassword { get; }
}
