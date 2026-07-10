using PersonalFinance.Domain.Filters.Transaction;
using PersonalFinance.Domain.ReadModels;
using PersonalFinance.Domain.ReadModels.Transaction;

namespace PersonalFinance.Domain.Repositories.Transaction;
using Entities;

public interface ITransactionReadRepository
{
    Task<PagedList<Transaction>> GetAll(Guid userId, TransactionFilter filter);
    Task<TransactionDashboard> GetDashboard(Guid userId, DateTime date);
}
