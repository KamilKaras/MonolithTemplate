using MonolithTemplate.Shared.Cqrs;
using MonolithTemplate.Shared.ResultPattern;

namespace MonolithTemplate.Identity.Application.Features.UserConfirmEmail
{
    public sealed class UserConfirmEmailCommand(string userId, string token) : ICommand<Result<Guid>>
    {
        public string UserId { get; } = userId;
        public string Token { get; } = token;
    }
}
