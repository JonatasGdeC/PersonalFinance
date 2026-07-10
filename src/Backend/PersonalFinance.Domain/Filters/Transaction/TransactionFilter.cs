using PersonalFinance.Domain.Enums;

namespace PersonalFinance.Domain.Filters.Transaction;

public record TransactionFilter
{
    public string? Search { get; set; }
    public ListOrder ListOrder { get; set; }
    public long? CategoryId { get; set; }
    public TransactionType? TransactionType { get; set; }
    public Pagination Pagination { get; set; } = new();
}
