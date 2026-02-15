using MonolithTemplate.Shared.Cqrs;
using MonolithTemplate.Shared.ResultPattern;

namespace MonolithTemplate.Identity.Application.Features.UserRegistration;

public sealed class UserRegistrationCommand : ICommand<Result<Guid>>
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
