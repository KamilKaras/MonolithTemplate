public interface IUnitOfWork
{
    Task Begin(CancellationToken ct);
    Task Commit(CancellationToken ct);
    Task Rollback(CancellationToken ct);
}
