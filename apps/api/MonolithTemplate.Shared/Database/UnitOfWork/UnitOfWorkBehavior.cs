using Microsoft.Extensions.Logging;
using MonolithTemplate.Shared.Messaging;

namespace MonolithTemplate.Shared.Database.UnitOfWork;

public sealed class UnitOfWorkBehavior<TRequest, TResponse>(IUnitOfWork uw, ILogger<UnitOfWorkBehavior<TRequest, TResponse>> logger) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, CancellationToken ct, RequestHandlerDelegate<TResponse> next)
    {
        if (request is not ICommand<TResponse>)
            return await next();

        logger.LogInformation($"Request handler started, {request.GetType().Name}");

        await uw.Begin(ct);

        try
        {
            var result = await next();
            await uw.Commit(ct);
            logger.LogInformation($"Request handler finished, {request.GetType().Name}");

            return result;
        }
        catch (Exception ex)
        {
            await uw.Rollback(ct);
            logger.LogInformation($"Request handler failed, {request.GetType().Name}", ex);
            throw;
        }
    }
}
