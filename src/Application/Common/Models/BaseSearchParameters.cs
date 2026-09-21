using Delex_POS.Application.Common.Enums;

namespace Delex_POS.Application.Common.Models;
public abstract class BaseSearchParameters
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public string? SearchQuery { get; set; }
    public string? SortBy { get; set; }
    public TableSort? Sort { get; set; }
}
