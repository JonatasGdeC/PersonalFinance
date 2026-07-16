using PersonalFinance.Communication.Dtos;

namespace PersonalFinance.Communication.Responses.Transaction;

public class GetTransactionDashboardResponse
{
    public List<TransactionDto> LastestTransactions { get; init; } = [];
    public double CurrentBalance { get; init; }
    public double TotalIncome { get; init; }
    public double TotalExpense { get; init; }
}