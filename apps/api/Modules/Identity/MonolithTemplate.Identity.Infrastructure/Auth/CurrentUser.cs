using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using MonolithTemplate.Identity.Application.Abstractions.Auth;

namespace MonolithTemplate.Identity.Infrastructure.Auth;

public class CurrentUser : ICurrentUser
{
    public CurrentUser(IHttpContextAccessor accessor)
    {
        UserId = accessor.HttpContext?.User?
            .FindFirst(ClaimTypes.NameIdentifier)?.Value;
    }
    public string? UserId {get;}
}
