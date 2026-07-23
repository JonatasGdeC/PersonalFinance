using PersonalFinance.Communication.Dtos;

namespace PersonalFinance.Communication.Responses.Transaction;

public record GetListTransactionsResponse : PaginationResponse
{
    public required List<TransactionDto> ListTransactions { get; init; }
    public double TotalAmount { get; init; }
}