using Microsoft.EntityFrameworkCore;
using PersonalFinance.Domain.Entities;
using PersonalFinance.Domain.Repositories.Budget;

namespace PersonalFinance.Infrastructure.DataAccess.Repositories;

internal class BudgetRepository(PersonalFinanceDbContext context) : IBudgetReadRepository, IBudgetWriteRepository
{
    public async Task<List<Budget>> GetAll(Guid userId)
    {
        return await context.Budgets
            .AsNoTracking()
            .Include(navigationPropertyPath: budget => budget.Category)
            .Where(predicate: budget => budget.UserId == userId)
            .ToListAsync();
    }

    public async Task Add(Budget budget)
    {
        await context.Budgets.AddAsync(entity: budget);
    }

    public void Update(Budget budget)
    {
        context.Budgets.Update(entity: budget);
    }

    public void Delete(Budget budget)
    {
        context.Budgets.Remove(entity: budget);
    }

    public async Task<Budget?> GetById(Guid budgetId, Guid userId)
    {
        return await context.Budgets.AsTracking()
            .FirstOrDefaultAsync(predicate: budget => budget.Id == budgetId && budget.UserId == userId);
    }
}
