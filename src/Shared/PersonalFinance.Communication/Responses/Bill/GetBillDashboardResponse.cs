namespace PersonalFinance.Communication.Responses.Bill;

public class GetBillDashboardResponse
{
    public decimal Total { get; init; }
    public decimal Paid { get; init; }
    public decimal Upcoming { get; init; }
    public decimal DueSoon { get; init; }
}