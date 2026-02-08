using System;
using MonolithTemplate.Shared.CQRS.Abstractions;

namespace MonolithTemplate.Shared.Database.UnitOfWork;

public sealed class UnitOfWorkBehavior<TRequest, TResponse>(IUnitOfWork uw) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, CancellationToken ct, RequestHandlerDelegate<TResponse> next)
    {
        if (request is not ICommand<TResponse>)
            return await next();

        await uw.Begin(ct);

        try
        {
            var result = await next();
            await uw.Commit(ct);
            return result;
        }
        catch
        {
            await uw.Rollback(ct);
            throw;
        }
    }
}
