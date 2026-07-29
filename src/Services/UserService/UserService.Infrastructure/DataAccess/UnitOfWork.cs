using UserService.Domain.Repositories;

namespace UserService.Infrastructure.DataAccess;

internal class UnitOfWork(UserServiceDbContext context) : IUnitOfWork
{
    public async Task Commit()
    {
        await context.SaveChangesAsync();
    }
}