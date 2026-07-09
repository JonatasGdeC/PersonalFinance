namespace PersonalFinance.Domain.Repositories.Budget;
using Entities;

public interface IBudgetWriteRepository
{
    Task Add(Budget budget);
    void Update(Budget budget);
    void Delete(Budget budget);
    Task<Budget?> GetById(long budgetId, Guid userId);
}