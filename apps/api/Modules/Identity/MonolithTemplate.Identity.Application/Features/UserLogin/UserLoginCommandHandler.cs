using Microsoft.AspNetCore.Identity;
using MonolithTemplate.Identity.Domain.IdentityModels;
using MonolithTemplate.Shared.Cqrs;
using MonolithTemplate.Shared.ResultPattern;

namespace MonolithTemplate.Identity.Application.Features.UserLogin;

public class UserLoginCommandHandler : IRequestHandler<UserLoginCommand, Result<Guid>>
{
    private readonly UserManager<User> _userManager;

    public UserLoginCommandHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }
    public async Task<Result<Guid>> Handle(UserLoginCommand request, CancellationToken ct)
    {
        return Result<Guid>.Success(Guid.NewGuid());
    }
}
