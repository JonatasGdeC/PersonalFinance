using PersonalFinance.Domain.Requests.Transaction;
using PersonalFinance.Domain.Response;

namespace PersonalFinance.Domain.Repositories.Transaction;
using Entities;

public interface ITransactionReadRepository
{
    Task<PagedListResponse<Transaction>> GetAll(Guid userId, GetAllTransactionRequest request);
}