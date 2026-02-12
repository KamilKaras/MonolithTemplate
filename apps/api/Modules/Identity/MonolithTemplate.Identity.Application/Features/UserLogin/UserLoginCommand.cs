using MonolithTemplate.Shared.Messaging;
using MonolithTemplate.Shared.ResultPattern;

namespace MonolithTemplate.Identity.Application.Features.UserLogin;

public sealed class UserLoginCommand : IRequest<Result<Guid>>
{
    public UserLoginCommand(string email, string password)
    {
        Email = email;
        Password = password;
    }

    public string Email { get; }
    public string Password { get; }

}
