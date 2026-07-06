using MonolithTemplate.Shared.Cqrs;
using MonolithTemplate.Shared.ResultPattern;

namespace MonolithTemplate.Identity.Application.Features.UserForgetPassword;

public sealed class UserForgetPasswordCommand : ICommand<Result> {
    public UserForgetPasswordCommand() {
        Email = string.Empty;
    }

    public UserForgetPasswordCommand(string email) {
        Email = email;
    }

    public string Email { get; init; }

}
