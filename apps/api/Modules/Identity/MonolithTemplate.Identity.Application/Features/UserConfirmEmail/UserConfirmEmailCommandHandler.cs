using Microsoft.AspNetCore.Identity;
using MonolithTemplate.Identity.Domain.IdentityModels;
using MonolithTemplate.Shared.Cqrs;
using MonolithTemplate.Shared.ResultPattern;

namespace MonolithTemplate.Identity.Application.Features.UserForgetPassword;

public class UserConfirmEmailCommandHandler : IRequestHandler<UserConfirmEmailCommand, Result<Guid>>
{
    private readonly UserManager<User> _userManager;

    public UserConfirmEmailCommandHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }
    public async Task<Result<Guid>> Handle(UserConfirmEmailCommand request, CancellationToken ct)
    {
        var user = await _userManager.FindByIdAsync(request.UserId);
        if (user is null)
            return Error.NotFound("UserConfirmEmail.NotFound", "Użytkownik nie istnieje!");

        if (user.EmailConfirmed)
            return Error.Validation("UserConfirmEmail.EmailConfirmed", "Email został już potwierdzony!");

        var result = await _userManager.ConfirmEmailAsync(user, request.Token);

        if (!result.Succeeded)
            return Error.Failure("UserConfirmEmail.NotSucceeded", "Wystąpił problem podczas potwierdzania email!");

        return Result<Guid>.Success(user.Id);
    }
}
