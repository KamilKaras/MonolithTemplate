using System;
using Microsoft.AspNetCore.Identity;
using MonolithTemplate.Identity.Infrastructure.IdentityModels;
using MonolithTemplate.Shared.CQRS.Abstractions;

namespace MonolithTemplate.Identity.Application.Features.UserRegistration;

public class UserRegistrationCommandHandler : IRequestHandler<UserRegistrationCommand, Guid>
{
    private readonly UserManager<User> _userManager;

    public UserRegistrationCommandHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }
    public async Task<Guid> Handle(UserRegistrationCommand request, CancellationToken ct)
    {
        var user = new User();
        var result = await _userManager.CreateAsync(user);
        return result.Succeeded ? Guid.NewGuid() : Guid.NewGuid();
    }
}
