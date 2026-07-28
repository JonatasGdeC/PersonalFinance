using FinanceService.Domain.Filters;
using FinanceService.Domain.ReadModels;

namespace FinanceService.Domain.Repositories.Bill;
using Entities;

public interface IBillReadRepository
{
    Task<PagedList<Bill>> GetAll(Guid userId, BillFilter filter);
    Task<BillDashboard> GetDashboard(Guid userId);
}
