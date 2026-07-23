using PersonalFinance.Domain.Enums;

namespace PersonalFinance.Domain.Filters;

public record TransactionFilter
{
    public string? Search { get; set; }
    public ListOrder ListOrder { get; set; }
    public Guid? CategoryId { get; set; }
    public TransactionType? TransactionType { get; set; }
    public Pagination Pagination { get; set; } = new();
}
