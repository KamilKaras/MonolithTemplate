using MonolithTemplate.Shared.Cqrs;
using MonolithTemplate.Shared.ResultPattern;

namespace MonolithTemplate.Identity.Application.Features.UserForgetPassword;

public sealed class ResetPasswordCommand : ICommand<Result> {

    public ResetPasswordCommand(
        string password,
         string confirmPassword,
         string userId,
         string token) {
        Password = password;
        ConfirmPassword = confirmPassword;
        UserId = userId;
        Token = token;
    }

    public string Password {
        get;
    }
    public string ConfirmPassword {
        get;
    }
    public string UserId {
        get;
    }
    public string Token {
        get;
    }
}
