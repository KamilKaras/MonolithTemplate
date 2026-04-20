using MonolithTemplate.Shared.Cqrs;
using MonolithTemplate.Shared.ResultPattern;

namespace MonolithTemplate.Identity.Application.Features.UserForgetPassword;

public sealed class ResetPasswordCommand : ICommand<Result> {

    public ResetPasswordCommand(string password, string confirmPassword) {
        Password = password;
        ConfirmPassword = confirmPassword;
    }

    public string Password {
        get;
    }
    public string ConfirmPassword {
        get;
    }
}
