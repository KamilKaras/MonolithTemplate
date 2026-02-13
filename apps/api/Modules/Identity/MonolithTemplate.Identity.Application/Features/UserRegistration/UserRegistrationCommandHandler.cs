using Microsoft.AspNetCore.Identity;
using MonolithTemplate.Identity.Domain.IdentityModels;
using MonolithTemplate.Shared.Cqrs;
using MonolithTemplate.Shared.ResultPattern;

namespace MonolithTemplate.Identity.Application.Features.UserRegistration;

public class UserRegistrationCommandHandler : IRequestHandler<UserRegistrationCommand, Result<Guid>>
{
    private readonly UserManager<User> _userManager;
    private readonly IPasswordHasher<User> _passwordHasher;

    public UserRegistrationCommandHandler(
        UserManager<User> userManager,
        IPasswordHasher<User> passwordHasher)
    {
        _userManager = userManager;
        _passwordHasher = passwordHasher;
    }
    public async Task<Result<Guid>> Handle(UserRegistrationCommand request, CancellationToken ct)
    {
        if (request.Password != request.ConfirmPassword)
            return Result<Guid>.Failure(Error.BadRequest("Identity.PasswordNotMatch", "Podane hasła nie są takie same!"));

        var user = new User
        {
            Email = request.Email,
            UserName = request.UserName
        };

        var result = await _userManager.CreateAsync(user, request.Password);
        return result.Succeeded ?
            Result<Guid>.Success(user.Id)
            :
            Result<Guid>.Failure(Error.Failure("Identity.RegistrationFailed", "Wystąpił błąd podczas rejestracji"));
    }
}
