using MonolithTemplate.Shared.Cqrs;
using MonolithTemplate.Shared.ResultPattern;

namespace MonolithTemplate.Identity.Application.Features.UserConfirmEmail;

public sealed class GetUserCredentialsQuery : IQuery<Result<Guid>>
{
    public GetUserCredentialsQuery(string userId)
    {
        UserId = userId;
    }

    public string UserId { get; }
}
