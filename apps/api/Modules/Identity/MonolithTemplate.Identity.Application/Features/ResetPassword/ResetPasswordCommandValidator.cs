using MonolithTemplate.Shared.ResultPattern;

namespace MonolithTemplate.Identity.Application.Features.ResetPassword;

public static class ResetPasswordCommandValidator
{
    public static Result Validate(ResetPasswordCommand command)
    {
        if (string.IsNullOrWhiteSpace(command.Password))
            return Error.Validation("Identity.ResetPassword.PasswordRequired", "Haslo jest wymagane.");

        if (string.IsNullOrWhiteSpace(command.ConfirmPassword))
            return Error.Validation("Identity.ResetPassword.ConfirmPasswordRequired", "Potwierdzenie hasla jest wymagane.");

        if (string.IsNullOrWhiteSpace(command.UserId))
            return Error.Validation("Identity.ResetPassword.UserIdRequired", "Id uzytkownika jest wymagane.");

        if (string.IsNullOrWhiteSpace(command.Token))
            return Error.Validation("Identity.ResetPassword.TokenRequired", "Token jest wymagany.");

        return Result.Success();
    }
}
