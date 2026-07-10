using PersonalFinance.Domain.Requests.Transaction;

namespace PersonalFinance.Domain.Repositories.Transaction;
using Entities;

public interface ITransactionReadRepository
{
    Task<PagedList<Transaction>> GetAll(Guid userId, GetAllTransactionRequest request);
}