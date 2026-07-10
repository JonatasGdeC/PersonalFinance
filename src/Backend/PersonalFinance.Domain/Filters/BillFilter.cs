using PersonalFinance.Domain.Enums;

namespace PersonalFinance.Domain.Filters;

public record BillFilter
{
    public string? Search { get; set; }
    public ListOrder ListOrder { get; set; }
    public Pagination Pagination { get; set; } = new();
}
