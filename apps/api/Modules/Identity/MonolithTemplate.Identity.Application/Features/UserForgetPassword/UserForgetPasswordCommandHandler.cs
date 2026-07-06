using Microsoft.AspNetCore.Identity;

using MonolithTemplate.Identity.Contracts;
using MonolithTemplate.Identity.Domain.IdentityModels;
using MonolithTemplate.Shared.Cqrs;
using MonolithTemplate.Shared.Events;
using MonolithTemplate.Shared.ResultPattern;

namespace MonolithTemplate.Identity.Application.Features.UserForgetPassword;

public class UserForgetPasswordCommandHandler(
    UserManager<User> userManager,
    IEventBus eventBus) : IRequestHandler<UserForgetPasswordCommand, Result> {

    public async Task<Result> Handle(UserForgetPasswordCommand request, CancellationToken ct) {
        var validationResult = UserForgetPasswordCommandValidator.Validate(request);
        if (!validationResult.IsSuccess)
            return validationResult;

        var user = await userManager.FindByEmailAsync(request.Email);

        if (user is not null) {
            var token = await userManager.GeneratePasswordResetTokenAsync(user);

            await eventBus.Publish(new UserPasswordResetIntegrationEvent(user.Id, request.Email, token));
        }

        return Result.Success();
    }
}
