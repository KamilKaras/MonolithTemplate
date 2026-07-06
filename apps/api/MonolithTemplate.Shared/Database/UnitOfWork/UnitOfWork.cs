using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace MonolithTemplate.Shared.Database.UnitOfWork;

public class UnitOfWork<TDbContext>(TDbContext db) : IUnitOfWork
    where TDbContext : DbContext
{
    private IDbContextTransaction? _tx;

    public async Task Begin(CancellationToken ct)
    {
        if (_tx is not null)
            throw new InvalidOperationException("Transaction already started.");

        _tx = await db.Database.BeginTransactionAsync(ct);
    }

    public async Task Commit(CancellationToken ct)
    {
        if (_tx is null)
            throw new InvalidOperationException("No active transaction to commit.");

        try
        {
            await db.SaveChangesAsync(ct);
            await _tx.CommitAsync(ct);
        }
        finally
        {
            await _tx.DisposeAsync();
            _tx = null;
        }
    }

    public async Task Rollback(CancellationToken ct)
    {
        if (_tx is null)
            return;

        try
        {
            await _tx.RollbackAsync(ct);
        }
        finally
        {
            await _tx.DisposeAsync();
            _tx = null;
        }
    }
}
