using MonolithTemplate.Shared.ResultPattern;
using MonolithTemplate.Identity.Application.Features.Validation;

namespace MonolithTemplate.Identity.Application.Features.UserLogin;

public static class UserLoginCommandValidator
{
    public static Result Validate(UserLoginCommand command)
    {
        if (string.IsNullOrWhiteSpace(command.Email))
            return Error.Validation("Auth.EmailRequired", "Email jest wymagany.");

        if (!IdentityValidationRules.IsValidEmail(command.Email))
            return Error.Validation("Auth.EmailInvalid", "Email ma nieprawidlowy format.");

        if (string.IsNullOrWhiteSpace(command.Password))
            return Error.Validation("Auth.PasswordRequired", "Haslo jest wymagane.");

        if (!IdentityValidationRules.HasMinimumPasswordLength(command.Password))
            return Error.Validation("Auth.PasswordTooShort", "Haslo musi miec minimum 8 znakow.");

        return Result.Success();
    }
}
