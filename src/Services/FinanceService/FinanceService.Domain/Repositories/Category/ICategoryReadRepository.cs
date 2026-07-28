using FinanceService.Domain.Enums;

namespace FinanceService.Domain.Repositories.Category;
using Entities;

public interface ICategoryReadRepository
{
    Task<List<Category>> GetAll(Guid userId, TransactionType? transactionType = null);
}