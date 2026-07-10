using PersonalFinance.Communication.Requests;
using PersonalFinance.Communication.Requests.Bill;

namespace PersonalFinance.Domain.Repositories.Bill;
using Entities;

public interface IBillReadRepository
{
    Task<PagedList<Bill>> GetAll(GetAllBillRequest request, Guid userId, PageRequest page);
}