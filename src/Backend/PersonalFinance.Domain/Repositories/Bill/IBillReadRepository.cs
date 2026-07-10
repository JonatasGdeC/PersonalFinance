using PersonalFinance.Domain.Filters.Bill;
using PersonalFinance.Domain.ReadModels;
using PersonalFinance.Domain.ReadModels.Bill;

namespace PersonalFinance.Domain.Repositories.Bill;
using Entities;

public interface IBillReadRepository
{
    Task<PagedList<Bill>> GetAll(Guid userId, BillFilter filter);
    Task<BillDashboard> GetDashboard(Guid userId);
}
