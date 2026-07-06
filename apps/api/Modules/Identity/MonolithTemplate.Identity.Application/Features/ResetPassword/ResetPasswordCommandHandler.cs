using Microsoft.AspNetCore.Identity;

using MonolithTemplate.Identity.Contracts;
using MonolithTemplate.Identity.Domain.IdentityModels;
using MonolithTemplate.Shared.Cqrs;
using MonolithTemplate.Shared.Events;
using MonolithTemplate.Shared.ResultPattern;

namespace MonolithTemplate.Identity.Application.Features.ResetPassword;

public class ResetPasswordCommandHandler(
    UserManager<User> userManager) : IRequestHandler<ResetPasswordCommand, Result> {

    public async Task<Result> Handle(ResetPasswordCommand request, CancellationToken ct) {
        var validationResult = ResetPasswordCommandValidator.Validate(request);
        if (!validationResult.IsSuccess)
            return validationResult;

        if (request.Password != request.ConfirmPassword)
            return Error.BadRequest("Identity.PasswordNotMatch", "Podane hasła nie są takie same!");

        var user = await userManager.FindByIdAsync(request.UserId);

        if (user is null)
            return Error.NotFound("ResetPasswordCommandHandler.Handle", "Uzytkownik do zresetowania hasła nie istnieje!");

        var result = await userManager.ResetPasswordAsync(user, request.Token, request.Password);

        if (!result.Succeeded)
            return Error.Failure("ResetPasswordCommandHandler.Handle", "Nie udało się zresetować hasła, spróbuj ponownie!");

        return Result.Success();
    }
}
