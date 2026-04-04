using Microsoft.AspNetCore.Identity;
using MonolithTemplate.Identity.Application.Abstractions.Auth;
using MonolithTemplate.Identity.Domain.IdentityModels;
using MonolithTemplate.Shared.Cqrs;
using MonolithTemplate.Shared.ResultPattern;

namespace MonolithTemplate.Identity.Application.Features.UserLogin;

public class UserLoginCommandHandler : IRequestHandler<UserLoginCommand, Result<UserLoginResponse>>
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly ITokenGenerator _tokenGenerator;

    public UserLoginCommandHandler(
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        ITokenGenerator tokenGenerator)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenGenerator = tokenGenerator;
    }
    public async Task<Result<UserLoginResponse>> Handle(UserLoginCommand request, CancellationToken ct)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user is null)
            return Error.Failure("Auth.InvalidCredentials", "Nieprawidłowy email lub hasło!");

        var signIn = await _signInManager.CheckPasswordSignInAsync(
            user, request.Password, lockoutOnFailure: true);

        if (signIn.IsLockedOut)
            return Error.Forbidden("Auth.LockedOut", "Konto jest chwilowo zablokowane. Spróbuj później.");

        if (!signIn.Succeeded)
            return Error.BadRequest("Auth.InvalidCredentials", "Nieprawidłowy email lub hasło");

        if (!await _userManager.IsEmailConfirmedAsync(user))
            return Result<UserLoginResponse>.Failure(
              Error.Forbidden("Auth.EmailNotConfirmed", "Potwierdź email, aby się zalogować."));

        var accessToken = _tokenGenerator.Generate(user);

        return new UserLoginResponse(accessToken,user.Id);
    }
}
