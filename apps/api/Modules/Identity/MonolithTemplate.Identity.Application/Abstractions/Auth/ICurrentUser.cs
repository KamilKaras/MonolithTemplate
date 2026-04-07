namespace MonolithTemplate.Identity.Application.Abstractions.Auth;

public interface ICurrentUser
{
    string? UserId { get; }
}
