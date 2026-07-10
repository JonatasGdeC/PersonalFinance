using PersonalFinance.Domain.Filters;
using PersonalFinance.Domain.ReadModels;

namespace PersonalFinance.Domain.Repositories.Bill;
using Entities;

public interface IBillReadRepository
{
    Task<PagedList<Bill>> GetAll(Guid userId, BillFilter filter);
    Task<BillDashboard> GetDashboard(Guid userId);
}
