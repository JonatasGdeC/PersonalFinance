using FinanceService.Domain.Entities;

namespace FinanceService.Domain.ReadModels;

public record TransactionDashboard
{
    public List<Transaction> LastestTransactions { get; init; } = [];
    public double CurrentBalance { get; init; }
    public double TotalIncome { get; init; }
    public double TotalExpense { get; init; }
}
