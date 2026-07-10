using PersonalFinance.Communication.Enums;

namespace PersonalFinance.Communication.Requests.Bill;

public record GetAllBillRequest
{
    public string? Search { get; set; }
    public ListOrder ListOrder { get; set; }
}