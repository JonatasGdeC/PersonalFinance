using PersonalFinance.Domain.Requests.Bill;

namespace PersonalFinance.Domain.Repositories.Bill;
using Entities;

public interface IBillReadRepository
{
    Task<PagedList<Bill>> GetAll(Guid userId, GetAllBillRequest request);
}