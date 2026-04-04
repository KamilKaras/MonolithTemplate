namespace MonolithTemplate.Identity.Application.Features.UserLogin;

public sealed record UserLoginResponse(string Token,Guid UserId);