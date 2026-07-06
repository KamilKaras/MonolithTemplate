using MonolithTemplate.Shared.ResultPattern;
using MonolithTemplate.Identity.Application.Features.Validation;

namespace MonolithTemplate.Identity.Application.Features.UserRegistration;

public static class UserRegistrationCommandValidator
{
    public static Result Validate(UserRegistrationCommand command)
    {
        if (string.IsNullOrWhiteSpace(command.UserName))
            return Error.Validation("Identity.Registration.UserNameRequired", "Nazwa uzytkownika jest wymagana.");

        if (!IdentityValidationRules.IsValidUserName(command.UserName))
            return Error.Validation("Identity.Registration.UserNameTooShort", "Nazwa uzytkownika musi miec minimum 2 znaki.");

        if (string.IsNullOrWhiteSpace(command.Email))
            return Error.Validation("Identity.Registration.EmailRequired", "Email jest wymagany.");

        if (!IdentityValidationRules.IsValidEmail(command.Email))
            return Error.Validation("Identity.Registration.EmailInvalid", "Email ma nieprawidlowy format.");

        if (string.IsNullOrWhiteSpace(command.Password))
            return Error.Validation("Identity.Registration.PasswordRequired", "Haslo jest wymagane.");

        if (!IdentityValidationRules.HasMinimumPasswordLength(command.Password))
            return Error.Validation("Identity.Registration.PasswordTooShort", "Haslo musi miec minimum 8 znakow.");

        if (string.IsNullOrWhiteSpace(command.ConfirmPassword))
            return Error.Validation("Identity.Registration.ConfirmPasswordRequired", "Potwierdzenie hasla jest wymagane.");

        if (!IdentityValidationRules.HasMinimumPasswordLength(command.ConfirmPassword))
            return Error.Validation("Identity.Registration.ConfirmPasswordTooShort", "Potwierdzenie hasla musi miec minimum 8 znakow.");

        if (command.Password != command.ConfirmPassword)
            return Error.Validation("Identity.Registration.PasswordsDoNotMatch", "Podane hasla nie sa takie same.");

        return Result.Success();
    }
}
