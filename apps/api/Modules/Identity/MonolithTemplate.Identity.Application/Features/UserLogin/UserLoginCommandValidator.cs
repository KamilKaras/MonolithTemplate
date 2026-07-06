using MonolithTemplate.Shared.ResultPattern;

namespace MonolithTemplate.Identity.Application.Features.UserLogin;

public static class UserLoginCommandValidator
{
    public static Result Validate(UserLoginCommand command)
    {
        if (string.IsNullOrWhiteSpace(command.Email))
            return Error.Validation("Auth.EmailRequired", "Email jest wymagany.");

        if (string.IsNullOrWhiteSpace(command.Password))
            return Error.Validation("Auth.PasswordRequired", "Haslo jest wymagane.");

        return Result.Success();
    }
}
