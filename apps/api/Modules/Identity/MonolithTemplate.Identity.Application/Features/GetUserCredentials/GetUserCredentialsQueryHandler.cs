using Microsoft.AspNetCore.Identity;
using MonolithTemplate.Identity.Domain.IdentityModels;
using MonolithTemplate.Shared.Cqrs;
using MonolithTemplate.Shared.ResultPattern;

namespace MonolithTemplate.Identity.Application.Features.GetUserCredentials;

public class GetUserCredentialsQueryHandler : IRequestHandler<GetUserCredentialsQuery, Result<Guid>>
{
    private readonly UserManager<User> _userManager;

    public GetUserCredentialsQueryHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }
    public async Task<Result<Guid>> Handle(GetUserCredentialsQuery request, CancellationToken ct)
    {
        return Guid.NewGuid();
    }

        

        
}
