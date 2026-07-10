namespace PersonalFinance.Domain.ReadModels.Bill;

public record BillDashboard
{
    public decimal Total { get; init; }
    public decimal Paid { get; init; }
    public decimal Upcoming { get; init; }
    public decimal DueSoon { get; init; }
}
