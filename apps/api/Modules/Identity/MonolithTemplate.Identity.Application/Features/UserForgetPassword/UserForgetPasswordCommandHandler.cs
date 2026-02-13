using Microsoft.AspNetCore.Identity;
using MonolithTemplate.Identity.Domain.IdentityModels;
using MonolithTemplate.Shared.Cqrs;
using MonolithTemplate.Shared.ResultPattern;

namespace MonolithTemplate.Identity.Application.Features.UserForgetPassword;

public class UserForgetPasswordCommandHandler : IRequestHandler<UserForgetPasswordCommand, Result<Guid>>
{
    private readonly UserManager<User> _userManager;

    public UserForgetPasswordCommandHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }
    public async Task<Result<Guid>> Handle(UserForgetPasswordCommand request, CancellationToken ct)
    {
        return Result<Guid>.Success(Guid.NewGuid());
    }
}
