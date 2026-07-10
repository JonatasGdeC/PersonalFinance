using PersonalFinance.Communication.Requests;

namespace PersonalFinance.Domain.Repositories.Bill;
using Entities;

public interface IBillReadRepository
{
    Task<PagedList<Bill>> GetAll(Guid userId, PageRequest page);
}