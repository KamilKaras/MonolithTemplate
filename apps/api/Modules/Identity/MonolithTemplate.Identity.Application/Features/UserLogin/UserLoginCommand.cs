using MonolithTemplate.Shared.Cqrs;
using MonolithTemplate.Shared.ResultPattern;

namespace MonolithTemplate.Identity.Application.Features.UserLogin;

public sealed class UserLoginCommand : ICommand<Result<UserLoginResponse>>
{
    public UserLoginCommand(string email, string password)
    {
        Email = email;
        Password = password;
    }

    public string Email { get; init; }
    public string Password { get; init; }

}
