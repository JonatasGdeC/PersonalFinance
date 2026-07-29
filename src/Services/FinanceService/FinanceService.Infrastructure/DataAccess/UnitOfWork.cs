using FinanceService.Domain.Repositories;

namespace FinanceService.Infrastructure.DataAccess;

internal class UnitOfWork(FinanceServiceDbContext context) : IUnitOfWork
{
    public async Task Commit()
    {
        await context.SaveChangesAsync();
    }
}