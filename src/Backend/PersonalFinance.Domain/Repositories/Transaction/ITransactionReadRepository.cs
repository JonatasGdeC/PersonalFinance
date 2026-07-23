using PersonalFinance.Domain.Filters;
using PersonalFinance.Domain.ReadModels;

namespace PersonalFinance.Domain.Repositories.Transaction;
using Entities;

public interface ITransactionReadRepository
{
    Task<PagedList<Transaction>> GetAll(Guid userId, TransactionFilter filter);
    Task<TransactionDashboard> GetDashboard(Guid userId, DateTime date);
    Task<PagedList<Transaction>> GetByCategory(Guid userId, Guid categoryId, DateTime date, Pagination pagination);
    Task<double> GetTotalAmountByCategory(Guid userId, Guid categoryId, DateTime date);
}
