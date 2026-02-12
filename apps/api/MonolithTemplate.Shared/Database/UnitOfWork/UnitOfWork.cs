using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

public class UnitOfWork<TDbContext>(TDbContext db) : IUnitOfWork
    where TDbContext : DbContext
{
    private IDbContextTransaction? _tx;

    public async Task Begin(CancellationToken ct)
        => _tx = await db.Database.BeginTransactionAsync(ct);

    public async Task Commit(CancellationToken ct)
    {
        await db.SaveChangesAsync(ct);
        if (_tx is not null) await _tx.CommitAsync(ct);
    }

    public async Task Rollback(CancellationToken ct)
    {
        if (_tx is not null) await _tx.RollbackAsync(ct);
    }
}
