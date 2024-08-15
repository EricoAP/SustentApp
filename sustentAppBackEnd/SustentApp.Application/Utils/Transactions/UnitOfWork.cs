using SustentApp.Application.Utils.Transactions.Abstractions;
using SustentApp.Infrastructure.Contexts;

namespace SustentApp.Application.Utils.Transactions;

public class UnitOfWork : IUnitOfWork
{
    private readonly SustentAppContext _context;

    public UnitOfWork(SustentAppContext context)
    {
        _context = context;
    }

    public void BeginTransaction() { }

    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
    }
    public void Rollback() { }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing) { }
}
