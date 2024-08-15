using SustentApp.Domain.Utils.Queries.Enums;

namespace SustentApp.Domain.Utils.Queries.Entities;

public class QueryFilter
{
    private int _pageSize = 10;
    public int Page { get; set; } = 1;
    public int PageSize
    {
        get { return _pageSize; }
        set { _pageSize = value > 100 ? 100 : value; }
    }
    public string OrderBy { get; set; } = "Id";
    public QueryOrderType OrderType { get; set; } = QueryOrderType.Ascending;
}
