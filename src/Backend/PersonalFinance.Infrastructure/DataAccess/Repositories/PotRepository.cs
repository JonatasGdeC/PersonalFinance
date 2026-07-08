using Microsoft.EntityFrameworkCore;
using PersonalFinance.Domain.Entities;
using PersonalFinance.Domain.Repositories.Pot;

namespace PersonalFinance.Infrastructure.DataAccess.Repositories;

internal class PotRepository(PersonalFinanceDbContext context) : IPotReadRepository, IPotWriteRepository
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

    public async Task<Pot?> GetById(long potId, Guid userId)
    {
        return await context.Pots.AsTracking().FirstOrDefaultAsync(predicate: pot => pot.Id == potId && pot.UserId == userId);
    }
}