using PersonalFinance.Domain.Enums;

namespace PersonalFinance.Domain.Requests.Bill;

public record GetAllBillRequest
{
    public string? Search { get; set; }
    public ListOrder ListOrder { get; set; }
    public PageRequest PageRequest { get; set; } = new();
}