using MonolithTemplate.Shared.ResultPattern;
using MonolithTemplate.Identity.Application.Features.Validation;

namespace MonolithTemplate.Identity.Application.Features.UserForgetPassword;

public static class UserForgetPasswordCommandValidator
{
    public static Result Validate(UserForgetPasswordCommand command)
    {
        if (string.IsNullOrWhiteSpace(command.Email))
            return Error.Validation("Identity.ForgetPassword.EmailRequired", "Email jest wymagany.");

        if (!IdentityValidationRules.IsValidEmail(command.Email))
            return Error.Validation("Identity.ForgetPassword.EmailInvalid", "Email ma nieprawidlowy format.");

        return Result.Success();
    }
}
