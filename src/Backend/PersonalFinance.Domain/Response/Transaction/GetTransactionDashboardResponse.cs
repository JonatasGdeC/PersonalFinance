namespace PersonalFinance.Domain.Response.Transaction;
using Entities;

public record GetTransactionDashboardResponse
{
    public List<Transaction> LastestTransactions { get; init; } = [];
    public double CurrentBalance { get; init; }
    public double TotalIncome { get; init; }
    public double TotalExpense { get; init; }
}