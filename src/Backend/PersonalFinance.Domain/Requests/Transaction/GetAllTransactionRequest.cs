using PersonalFinance.Domain.Enums;

namespace PersonalFinance.Domain.Requests.Transaction;

public record GetAllTransactionRequest
{
    public string? Search { get; set; }
    public ListOrder ListOrder { get; set; }
    public long? CategoryId { get; set; }
    public TransactionType? TransactionType { get; set; }
    public PageRequest PageRequest { get; set; } = new();
}