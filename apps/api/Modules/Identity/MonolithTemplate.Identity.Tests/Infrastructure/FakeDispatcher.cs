using MonolithTemplate.Identity.Application.Features.GetUserCredentials;
using MonolithTemplate.Identity.Application.Features.ResetPassword;
using MonolithTemplate.Identity.Application.Features.UserConfirmEmail;
using MonolithTemplate.Identity.Application.Features.UserForgetPassword;
using MonolithTemplate.Identity.Application.Features.UserLogin;
using MonolithTemplate.Identity.Application.Features.UserRegistration;
using MonolithTemplate.Shared.Cqrs;
using MonolithTemplate.Shared.ResultPattern;

namespace MonolithTemplate.Identity.Tests.Infrastructure;

public sealed class FakeDispatcher : IDispatcher
{
    public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken ct = default)
    {
        object response = request switch
        {
            UserRegistrationCommand cmd => HandleRegister(cmd),
            UserLoginCommand cmd => HandleLogin(cmd),
            UserForgetPasswordCommand cmd => HandleForgotPassword(cmd),
            ResetPasswordCommand cmd => HandleResetPassword(cmd),
            UserConfirmEmailCommand cmd => HandleConfirmEmail(cmd),
            GetUserCredentialsQuery => Result<GetUserCredentialsDto>.Success(new GetUserCredentialsDto(Guid.NewGuid(), "User")),
            _ => throw new InvalidOperationException($"Unhandled request type: {request.GetType().FullName}")
        };

        return Task.FromResult((TResponse)response);
    }

    private static Result<Guid> HandleRegister(UserRegistrationCommand cmd)
    {
        if (string.IsNullOrWhiteSpace(cmd.UserName))
            return Error.Validation("Identity.Registration.UserNameRequired", "Nazwa uzytkownika jest wymagana.");

        if (string.IsNullOrWhiteSpace(cmd.Email))
            return Error.Validation("Identity.Registration.EmailRequired", "Email jest wymagany.");

        if (string.IsNullOrWhiteSpace(cmd.Password))
            return Error.Validation("Identity.Registration.PasswordRequired", "Haslo jest wymagane.");

        if (string.IsNullOrWhiteSpace(cmd.ConfirmPassword))
            return Error.Validation("Identity.Registration.ConfirmPasswordRequired", "Potwierdzenie hasla jest wymagane.");

        return Result<Guid>.Success(Guid.NewGuid());
    }

    private static Result<UserLoginResponse> HandleLogin(UserLoginCommand cmd)
    {
        if (string.IsNullOrWhiteSpace(cmd.Email))
            return Error.Validation("Auth.EmailRequired", "Email jest wymagany.");

        if (string.IsNullOrWhiteSpace(cmd.Password))
            return Error.Validation("Auth.PasswordRequired", "Haslo jest wymagane.");

        return Result<UserLoginResponse>.Success(new UserLoginResponse("test-token"));
    }

    private static Result HandleForgotPassword(UserForgetPasswordCommand cmd)
    {
        if (string.IsNullOrWhiteSpace(cmd.Email))
            return Error.Validation("Identity.ForgetPassword.EmailRequired", "Email jest wymagany.");

        return Result.Success();
    }

    private static Result HandleResetPassword(ResetPasswordCommand cmd)
    {
        if (string.IsNullOrWhiteSpace(cmd.Password))
            return Error.Validation("Identity.ResetPassword.PasswordRequired", "Haslo jest wymagane.");

        if (string.IsNullOrWhiteSpace(cmd.ConfirmPassword))
            return Error.Validation("Identity.ResetPassword.ConfirmPasswordRequired", "Potwierdzenie hasla jest wymagane.");

        if (string.IsNullOrWhiteSpace(cmd.UserId))
            return Error.Validation("Identity.ResetPassword.UserIdRequired", "Id uzytkownika jest wymagane.");

        if (string.IsNullOrWhiteSpace(cmd.Token))
            return Error.Validation("Identity.ResetPassword.TokenRequired", "Token jest wymagany.");

        return Result.Success();
    }

    private static Result<Guid> HandleConfirmEmail(UserConfirmEmailCommand cmd)
    {
        if (string.IsNullOrWhiteSpace(cmd.UserId))
            return Error.Validation("Identity.ConfirmEmail.UserIdRequired", "Id uzytkownika jest wymagane.");

        if (string.IsNullOrWhiteSpace(cmd.Token))
            return Error.Validation("Identity.ConfirmEmail.TokenRequired", "Token jest wymagany.");

        return Result<Guid>.Success(Guid.NewGuid());
    }
}
