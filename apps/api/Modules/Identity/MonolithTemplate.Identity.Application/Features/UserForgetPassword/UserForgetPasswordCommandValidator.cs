using MonolithTemplate.Shared.ResultPattern;

namespace MonolithTemplate.Identity.Application.Features.UserForgetPassword;

public static class UserForgetPasswordCommandValidator
{
    public static Result Validate(UserForgetPasswordCommand command)
    {
        if (string.IsNullOrWhiteSpace(command.Email))
            return Error.Validation("Identity.ForgetPassword.EmailRequired", "Email jest wymagany.");

        return Result.Success();
    }
}
