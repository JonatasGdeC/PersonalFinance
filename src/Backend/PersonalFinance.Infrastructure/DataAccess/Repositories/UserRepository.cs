using Microsoft.EntityFrameworkCore;
using PersonalFinance.Domain.Entities;
using PersonalFinance.Domain.Repositories.User;

namespace PersonalFinance.Infrastructure.DataAccess.Repositories;

internal class UserRepository(PersonalFinanceDbContext context) : IUserReadRepository, IUserWriteRepository
{
    public async Task<User?> GetByEmail(string email)
    {
        return await context.Users.AsNoTracking().FirstOrDefaultAsync(predicate: user => user.Email == email);
    }

    public async Task Add(User user)
    {
        await context.Users.AddAsync(entity: user);
    }

    public void Update(User user)
    {
        context.Users.Update(entity: user);
    }

    public void Delete(User user)
    {
        context.Users.Remove(entity: user);
    }

    public async Task<User?> GetById(Guid id)
    {
        return await context.Users.FirstOrDefaultAsync(predicate: user => user.Id == id);
    }
}