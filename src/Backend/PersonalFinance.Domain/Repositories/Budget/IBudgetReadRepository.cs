namespace PersonalFinance.Domain.Repositories.Budget;
using Entities;

public interface IBudgetReadRepository
{
    Task<List<Budget>> GetAll(Guid userId);
}