using PersonalFinance.Communication.Enums;

namespace PersonalFinance.Communication.Requests.Bill;

public record BillFilterRequest
{
    public string? Search { get; set; }
    public ListOrder ListOrder { get; set; }
    public PaginationRequest Pagination { get; set; } = new();
}