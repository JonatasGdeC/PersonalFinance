using PersonalFinance.Domain.Requests.Bill;
using PersonalFinance.Domain.Response;
using PersonalFinance.Domain.Response.Bill;

namespace PersonalFinance.Domain.Repositories.Bill;
using Entities;

public interface IBillReadRepository
{
    Task<PagedListResponse<Bill>> GetAll(Guid userId, GetAllBillRequest request);
    Task<GetBillDashboardResponse> GetDashboard(Guid userId);
}