namespace PersonalFinance.Domain.ReadModels.Transaction;
using Entities;

public record TransactionDashboard
{
    public List<Transaction> LastestTransactions { get; init; } = [];
    public double CurrentBalance { get; init; }
    public double TotalIncome { get; init; }
    public double TotalExpense { get; init; }
}
