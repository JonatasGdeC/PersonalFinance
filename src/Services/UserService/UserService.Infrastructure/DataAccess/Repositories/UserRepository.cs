using Microsoft.EntityFrameworkCore;
using UserService.Domain.Entities;
using UserService.Domain.Repositories.User;

namespace UserService.Infrastructure.DataAccess.Repositories;

internal class UserRepository(UserServiceDbContext context) : IUserReadRepository, IUserWriteRepository
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