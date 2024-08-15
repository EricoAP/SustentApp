using SustentApp.Domain.Utils.Queries.Entities;
using SustentApp.Domain.Utils.Queries.Enums;
using System.Linq.Expressions;

namespace SustentApp.Domain.Utils.Repositories.Abstractions;

public interface IGenericRepository<T> where T : class
{
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);
    T Get(string id);
    T Get(Expression<Func<T, bool>> predicate);
    IQueryable<T> Query();
    PagedList<T> Paginate(IQueryable<T> query, int page, int pageSize, string orderBy, QueryOrderType orderType);
    Task AddAsync(T entity);
}
