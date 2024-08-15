namespace SustentApp.Domain.Utils.Queries.Entities;

public class PagedList<T> where T : class
{
    public List<T> Items { get; set; }
    public Pagination Pagination { get; set; }

    public PagedList() { }
}
