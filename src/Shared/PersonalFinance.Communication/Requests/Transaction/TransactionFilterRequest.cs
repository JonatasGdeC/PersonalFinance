using PersonalFinance.Communication.Enums;

namespace PersonalFinance.Communication.Requests.Transaction;

public record TransactionFilterRequest
{
    public string? Search { get; set; }
    public ListOrder ListOrder { get; set; }
    public long? CategoryId { get; set; }
    public TransactionType? TransactionType { get; set; }
    public PaginationRequest Pagination { get; set; } = new();
}