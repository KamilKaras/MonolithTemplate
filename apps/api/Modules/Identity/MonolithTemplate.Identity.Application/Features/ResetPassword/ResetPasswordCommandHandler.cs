using Microsoft.AspNetCore.Identity;

using MonolithTemplate.Identity.Contracts;
using MonolithTemplate.Identity.Domain.IdentityModels;
using MonolithTemplate.Shared.Cqrs;
using MonolithTemplate.Shared.Events;
using MonolithTemplate.Shared.ResultPattern;

namespace MonolithTemplate.Identity.Application.Features.UserForgetPassword;

public class ResetPasswordCommandHandler(
    UserManager<User> userManager,
    IEventBus eventBus) : IRequestHandler<UserForgetPasswordCommand, Result> {

    public async Task<Result> Handle(UserForgetPasswordCommand request, CancellationToken ct) {


        return Result.Success();
    }
}
