using PersonalFinance.Communication.Dtos;
using PersonalFinance.Communication.Requests.Bill;
using PersonalFinance.Communication.Responses.Bill;

namespace PersonalFinance.Adapter.Interfaces;

public interface IBillServiceApi
{
    Task<BillDto> Register(RegisterBillRequest request);
    Task Update(Guid billId, RegisterBillRequest request);
    Task Delete(Guid billId);
    Task<GetAllBillResponse?> GetAll(BillFilterRequest filter);
    Task<GetBillDashboardResponse?> GetDashboard();
}
