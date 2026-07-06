using MonolithTemplate.Shared.ResultPattern;
using MonolithTemplate.Identity.Application.Features.Validation;

namespace MonolithTemplate.Identity.Application.Features.ResetPassword;

public static class ResetPasswordCommandValidator
{
    public static Result Validate(ResetPasswordCommand command)
    {
        if (string.IsNullOrWhiteSpace(command.Password))
            return Error.Validation("Identity.ResetPassword.PasswordRequired", "Haslo jest wymagane.");

        if (!IdentityValidationRules.HasMinimumPasswordLength(command.Password))
            return Error.Validation("Identity.ResetPassword.PasswordTooShort", "Haslo musi miec minimum 8 znakow.");

        if (string.IsNullOrWhiteSpace(command.ConfirmPassword))
            return Error.Validation("Identity.ResetPassword.ConfirmPasswordRequired", "Potwierdzenie hasla jest wymagane.");

        if (!IdentityValidationRules.HasMinimumPasswordLength(command.ConfirmPassword))
            return Error.Validation("Identity.ResetPassword.ConfirmPasswordTooShort", "Potwierdzenie hasla musi miec minimum 8 znakow.");

        if (command.Password != command.ConfirmPassword)
            return Error.Validation("Identity.ResetPassword.PasswordsDoNotMatch", "Podane hasla nie sa takie same.");

        if (string.IsNullOrWhiteSpace(command.UserId))
            return Error.Validation("Identity.ResetPassword.UserIdRequired", "Id uzytkownika jest wymagane.");

        if (!IdentityValidationRules.IsValidUserId(command.UserId))
            return Error.Validation("Identity.ResetPassword.UserIdInvalid", "Id uzytkownika ma nieprawidlowy format.");

        if (string.IsNullOrWhiteSpace(command.Token))
            return Error.Validation("Identity.ResetPassword.TokenRequired", "Token jest wymagany.");

        if (!IdentityValidationRules.IsPresentToken(command.Token))
            return Error.Validation("Identity.ResetPassword.TokenInvalid", "Token jest wymagany.");

        return Result.Success();
    }
}
