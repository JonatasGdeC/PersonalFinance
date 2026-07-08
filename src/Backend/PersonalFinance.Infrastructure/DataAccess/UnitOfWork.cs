using PersonalFinance.Domain.Repositories;

namespace PersonalFinance.Infrastructure.DataAccess;

internal class UnitOfWork(PersonalFinanceDbContext context) : IUnitOfWork
{
    public async Task Commit()
    {
        await context.SaveChangesAsync();
    }
}