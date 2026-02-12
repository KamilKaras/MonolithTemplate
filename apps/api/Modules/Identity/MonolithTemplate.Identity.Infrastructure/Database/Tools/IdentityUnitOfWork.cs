using MonolithTemplate.Identity.Application.UnitOfWork;
using MonolithTemplate.Shared.Database.UnitOfWork;
using MonolithTemplate.Shared.Messaging;
using Microsoft.Extensions.Logging;
using MonolithTemplate.Shared.OutboxPattern;

namespace MonolithTemplate.Identity.Infrastructure.Database.Tools;

internal sealed class IdentityUnitOfWork : UnitOfWork<MyIdentityDbContext>, IIdentityUnitOfWork
{
  public IdentityUnitOfWork(MyIdentityDbContext db) : base(db) { }
}

public sealed class IdentityUnitOfWorkBehavior<TRequest, TResponse>(
  IIdentityUnitOfWork uw,
  IOutbox outbox,
  IIdentityOutboxWriter outboxWriter,
  ILogger<UnitOfWorkBehavior<TRequest, TResponse, IIdentityUnitOfWork>> logger
) : UnitOfWorkBehavior<TRequest, TResponse, IIdentityUnitOfWork>(uw, outbox, outboxWriter, logger)
  where TRequest : IRequest<TResponse>;
