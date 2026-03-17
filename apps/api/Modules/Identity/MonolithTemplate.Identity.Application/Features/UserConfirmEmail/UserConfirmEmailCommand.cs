using MonolithTemplate.Shared.Cqrs;
using MonolithTemplate.Shared.ResultPattern;

namespace MonolithTemplate.Identity.Application.Features.UserForgetPassword;

public sealed class UserConfirmEmailCommand : ICommand<Result<Guid>>
{
    public UserConfirmEmailCommand(string userId, string token)
    {
        UserId = userId;
        Token = token;
    }

    public string UserId { get; }
    public string Token { get; }
}
