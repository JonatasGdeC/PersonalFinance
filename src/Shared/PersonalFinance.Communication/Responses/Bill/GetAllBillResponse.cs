using PersonalFinance.Communication.Dtos;

namespace PersonalFinance.Communication.Responses.Bill;

public record GetAllBillResponse : PaginationResponse
{
    public List<BillDto> ListBills { get; init; } = [];
}