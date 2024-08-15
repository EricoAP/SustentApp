using SustentApp.Domain.Utils.Queries.Entities;
using SustentApp.Domain.Utils.Queries.Enums;
using SustentApp.Domain.Utils.Repositories.Abstractions;
using SustentApp.Infrastructure.Contexts;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace SustentApp.Infrastructure.Utils.Repositories;

public class EFGenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly SustentAppContext _context;

    public EFGenericRepository(SustentAppContext context)
    {
        _context = context;
    }

    public void Add(T entity)
    {
        _context.Set<T>().Add(entity);
    }

    public void Update(T entity)
    {
        _context.Set<T>().Update(entity);
    }

    public void Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
    }

    public T Get(string id)
    {
        return _context.Set<T>().Find(id);
    }

    public T Get(Expression<Func<T, bool>> predicate)
    {
        return _context.Set<T>().FirstOrDefault(predicate);
    }

    public IQueryable<T> Query()
    {
        return _context.Set<T>().AsQueryable();
    }

    public PagedList<T> Paginate(IQueryable<T> query, int page, int pageSize, string orderBy, QueryOrderType orderType)
    {
        query = query
            .OrderBy($"{orderBy} {GetOrderTypeText(orderType)}")
            .Take(pageSize * page)
            .Skip(pageSize * (page - 1));

        return new PagedList<T>
        {
            Items = query.ToList(),
            Pagination = new Pagination(page, pageSize, query.LongCount())
        };
    }

    private string GetOrderTypeText(QueryOrderType orderType)
    {
        return orderType switch
        {
            QueryOrderType.Ascending => "Asc",
            QueryOrderType.Descending => "Desc",
            _ => "Asc"
        };
    }

    public async Task AddAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
    }
}
