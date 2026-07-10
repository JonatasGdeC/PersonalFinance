namespace PersonalFinance.Domain.Response.Bill;

public record GetBillDashboardResponse
{
    public decimal Total { get; init; }
    public decimal Paid { get; init; }
    public decimal Upcoming { get; init; }
    public decimal DueSoon { get; init; }
}