using Microsoft.Extensions.Logging;
using MonolithTemplate.Shared.Cqrs;
using MonolithTemplate.Shared.OutboxPattern;

namespace MonolithTemplate.Shared.Database.UnitOfWork;

public class UnitOfWorkBehavior<TRequest, TResponse, TUow>(
    TUow uw,
    IOutbox outbox,
    IOutboxWriter outboxWriter,
    ILogger<UnitOfWorkBehavior<TRequest, TResponse, TUow>> logger) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TUow : IUnitOfWork
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
            var events = outbox.DequeueAll();
            if (events.Count > 0)
                await outboxWriter.WriteAsync(events, ct);

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
