using Microsoft.EntityFrameworkCore;
using FinanceService.Domain.Entities;
using FinanceService.Domain.Repositories.Pot;

namespace FinanceService.Infrastructure.DataAccess.Repositories;

internal class PotRepository(FinanceServiceDbContext context) : IPotReadRepository, IPotWriteRepository
{
    public async Task<List<Pot>> GetAll(Guid userId)
    {
        return await context.Pots.AsTracking().Where(predicate: pot => pot.UserId == userId).ToListAsync();
    }

    public async Task Add(Pot pot)
    {
        await context.Pots.AddAsync(entity: pot);
    }

    public void Update(Pot pot)
    {
        context.Pots.Update(entity: pot);
    }

    public void Delete(Pot pot)
    {
        context.Pots.Remove(entity: pot);
    }

    public async Task<Pot?> GetById(Guid potId, Guid userId)
    {
        return await context.Pots.AsTracking().FirstOrDefaultAsync(predicate: pot => pot.Id == potId && pot.UserId == userId);
    }
}