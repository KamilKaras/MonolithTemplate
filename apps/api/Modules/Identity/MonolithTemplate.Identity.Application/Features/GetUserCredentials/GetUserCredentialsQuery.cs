using MonolithTemplate.Shared.Cqrs;
using MonolithTemplate.Shared.ResultPattern;

namespace MonolithTemplate.Identity.Application.Features.GetUserCredentials;

public sealed class GetUserCredentialsQuery : IQuery<Result<Guid>>
{
    public GetUserCredentialsQuery()
    {
    }

}
