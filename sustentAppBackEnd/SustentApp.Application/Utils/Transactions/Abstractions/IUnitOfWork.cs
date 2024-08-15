namespace SustentApp.Application.Utils.Transactions.Abstractions;

public interface IUnitOfWork : IDisposable
{
    void BeginTransaction();
    Task CommitAsync();
    void Rollback();
}
