using MonolithTemplate.Shared.Cqrs;
using MonolithTemplate.Shared.ResultPattern;

namespace MonolithTemplate.Identity.Application.Features.ResetPassword;

public sealed class ResetPasswordCommand : ICommand<Result> {

    public ResetPasswordCommand() {
        Password = string.Empty;
        ConfirmPassword = string.Empty;
        UserId = string.Empty;
        Token = string.Empty;
    }

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
        get; init;
    }
    public string ConfirmPassword {
        get; init;
    }
    public string UserId {
        get; init;
    }
    public string Token {
        get; init;
    }
}
