using MonolithTemplate.Shared.ResultPattern;

namespace MonolithTemplate.Identity.Application.Features.UserRegistration;

public static class UserRegistrationCommandValidator
{
    public static Result Validate(UserRegistrationCommand command)
    {
        if (string.IsNullOrWhiteSpace(command.UserName))
            return Error.Validation("Identity.Registration.UserNameRequired", "Nazwa uzytkownika jest wymagana.");

        if (string.IsNullOrWhiteSpace(command.Email))
            return Error.Validation("Identity.Registration.EmailRequired", "Email jest wymagany.");

        if (string.IsNullOrWhiteSpace(command.Password))
            return Error.Validation("Identity.Registration.PasswordRequired", "Haslo jest wymagane.");

        if (string.IsNullOrWhiteSpace(command.ConfirmPassword))
            return Error.Validation("Identity.Registration.ConfirmPasswordRequired", "Potwierdzenie hasla jest wymagane.");

        return Result.Success();
    }
}
