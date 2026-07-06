using MonolithTemplate.Shared.ResultPattern;
using MonolithTemplate.Identity.Application.Features.Validation;

namespace MonolithTemplate.Identity.Application.Features.UserConfirmEmail;

public static class UserConfirmEmailCommandValidator
{
    public static Result Validate(UserConfirmEmailCommand command)
    {
        if (string.IsNullOrWhiteSpace(command.UserId))
            return Error.Validation("Identity.ConfirmEmail.UserIdRequired", "Id uzytkownika jest wymagane.");

        if (!IdentityValidationRules.IsValidUserId(command.UserId))
            return Error.Validation("Identity.ConfirmEmail.UserIdInvalid", "Id uzytkownika ma nieprawidlowy format.");

        if (string.IsNullOrWhiteSpace(command.Token))
            return Error.Validation("Identity.ConfirmEmail.TokenRequired", "Token jest wymagany.");

        if (!IdentityValidationRules.IsPresentToken(command.Token))
            return Error.Validation("Identity.ConfirmEmail.TokenInvalid", "Token jest wymagany.");

        return Result.Success();
    }
}
