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
        var validation = UserRegistrationCommandValidator.Validate(cmd);
        if (!validation.IsSuccess)
            return validation.Error!;

        return Result<Guid>.Success(Guid.NewGuid());
    }

    private static Result<UserLoginResponse> HandleLogin(UserLoginCommand cmd)
    {
        var validation = UserLoginCommandValidator.Validate(cmd);
        if (!validation.IsSuccess)
            return validation.Error!;

        return Result<UserLoginResponse>.Success(new UserLoginResponse("test-token"));
    }

    private static Result HandleForgotPassword(UserForgetPasswordCommand cmd)
    {
        var validation = UserForgetPasswordCommandValidator.Validate(cmd);
        if (!validation.IsSuccess)
            return validation;

        return Result.Success();
    }

    private static Result HandleResetPassword(ResetPasswordCommand cmd)
    {
        var validation = ResetPasswordCommandValidator.Validate(cmd);
        if (!validation.IsSuccess)
            return validation;

        return Result.Success();
    }

    private static Result<Guid> HandleConfirmEmail(UserConfirmEmailCommand cmd)
    {
        var validation = UserConfirmEmailCommandValidator.Validate(cmd);
        if (!validation.IsSuccess)
            return validation.Error!;

        return Result<Guid>.Success(Guid.NewGuid());
    }
}
