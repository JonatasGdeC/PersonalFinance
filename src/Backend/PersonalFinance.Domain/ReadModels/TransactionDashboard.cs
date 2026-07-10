using PersonalFinance.Domain.Entities;

namespace PersonalFinance.Domain.ReadModels;

public record TransactionDashboard
{
    public List<Transaction> LastestTransactions { get; init; } = [];
    public double CurrentBalance { get; init; }
    public double TotalIncome { get; init; }
    public double TotalExpense { get; init; }
}
