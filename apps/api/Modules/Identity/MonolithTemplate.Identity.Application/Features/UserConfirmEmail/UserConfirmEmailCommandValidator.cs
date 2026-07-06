using MonolithTemplate.Shared.ResultPattern;

namespace MonolithTemplate.Identity.Application.Features.UserConfirmEmail;

public static class UserConfirmEmailCommandValidator
{
    public static Result Validate(UserConfirmEmailCommand command)
    {
        if (string.IsNullOrWhiteSpace(command.UserId))
            return Error.Validation("Identity.ConfirmEmail.UserIdRequired", "Id uzytkownika jest wymagane.");

        if (string.IsNullOrWhiteSpace(command.Token))
            return Error.Validation("Identity.ConfirmEmail.TokenRequired", "Token jest wymagany.");

        return Result.Success();
    }
}
