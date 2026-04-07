using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using MonolithTemplate.Identity.Application.Abstractions.Auth;
using MonolithTemplate.Identity.Domain.IdentityModels;
using MonolithTemplate.Shared.Cqrs;
using MonolithTemplate.Shared.ResultPattern;

namespace MonolithTemplate.Identity.Application.Features.GetUserCredentials;

public class GetUserCredentialsQueryHandler : IRequestHandler<GetUserCredentialsQuery, Result<GetUserCredentialsDto>>
{
    private readonly UserManager<User> _userManager;
    private readonly ICurrentUser _currentUser;

    public GetUserCredentialsQueryHandler(
        UserManager<User> userManager,
        ICurrentUser currentUser)
    {
        _userManager = userManager;
        _currentUser = currentUser;
    }
    public async Task<Result<GetUserCredentialsDto>> Handle(GetUserCredentialsQuery request, CancellationToken ct)
    {
       var userId = _currentUser.UserId;

        if (userId is null)
            return Error.Validation("UserCredentials.UserIdNotFound","Nie pobrano Id użytkownika");

        var user = await _userManager.FindByIdAsync(userId);

        if(user is null)
            return Error.Validation("UserCredentials.UserNotFound","Nie znaleziono użytkownika");

        return new GetUserCredentialsDto(user.Id, user.UserName!);
    }     
}
