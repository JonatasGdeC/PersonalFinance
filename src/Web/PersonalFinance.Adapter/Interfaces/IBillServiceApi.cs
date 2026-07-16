using PersonalFinance.Communication.Dtos;
using PersonalFinance.Communication.Requests.Bill;
using PersonalFinance.Communication.Responses.Bill;

namespace PersonalFinance.Adapter.Interfaces;

public interface IBillServiceApi
{
    Task<BillDto> Register(RegisterBillRequest request);
    Task Update(long billId, RegisterBillRequest request);
    Task Delete(long billId);
    Task<GetAllBillResponse?> GetAll(BillFilterRequest filter);
    Task<GetBillDashboardResponse?> GetDashboard();
}
