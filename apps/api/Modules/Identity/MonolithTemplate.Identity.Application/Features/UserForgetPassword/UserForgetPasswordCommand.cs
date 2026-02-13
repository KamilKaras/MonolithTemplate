using MonolithTemplate.Shared.Cqrs;
using MonolithTemplate.Shared.ResultPattern;

namespace MonolithTemplate.Identity.Application.Features.UserForgetPassword;

public sealed class UserForgetPasswordCommand : IRequest<Result<Guid>>
{
    public UserForgetPasswordCommand(string email)
    {
        Email = email;
    }

    public string Email { get; }

}
