using PersonalFinance.Communication.Requests;
using PersonalFinance.Communication.Requests.Transaction;

namespace PersonalFinance.Domain.Repositories.Transaction;
using Entities;

public interface ITransactionReadRepository
{
    Task<PagedList<Transaction>> GetAll(GetAllTransactionRequest request, Guid userId, PageRequest page);
}