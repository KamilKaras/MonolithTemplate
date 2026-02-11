using Microsoft.AspNetCore.Identity;
using MonolithTemplate.Identity.Domain.IdentityModels;
using MonolithTemplate.Shared.Messaging;
using MonolithTemplate.Shared.ResultPattern;

namespace MonolithTemplate.Identity.Application.Features.UserRegistration;

public class UserRegistrationCommandHandler : IRequestHandler<UserRegistrationCommand, Result<Guid>>
{
    private readonly UserManager<User> _userManager;

    public UserRegistrationCommandHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }
    public async Task<Result<Guid>> Handle(UserRegistrationCommand request, CancellationToken ct)
    {
        if (request.Password != request.ConfirmPassword)
            return Result<Guid>.Failure(Error.BadRequest("Identity.PasswordNotMatch", "Podane hasła nie są takie same!"));

        var user = new User();
        var result = await _userManager.CreateAsync(user);
        return result.Succeeded ?
            Result<Guid>.Success(user.Id)
            :
            Result<Guid>.Failure(Error.Failure("Identity.RegistrationFailed", "Wystąpił błąd podczas rejestracji"));
    }
}
