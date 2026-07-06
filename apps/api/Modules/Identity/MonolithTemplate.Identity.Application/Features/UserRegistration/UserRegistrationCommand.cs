using MonolithTemplate.Shared.Cqrs;
using MonolithTemplate.Shared.ResultPattern;

namespace MonolithTemplate.Identity.Application.Features.UserRegistration;

public sealed class UserRegistrationCommand : ICommand<Result<Guid>>
{
    public UserRegistrationCommand()
    {
        UserName = string.Empty;
        Email = string.Empty;
        Password = string.Empty;
        ConfirmPassword = string.Empty;
    }

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

    public string UserName { get; init; }
    public string Email { get; init; }
    public string Password { get; init; }
    public string ConfirmPassword { get; init; }
}
