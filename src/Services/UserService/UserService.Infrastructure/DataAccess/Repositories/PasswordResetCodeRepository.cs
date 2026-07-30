using Microsoft.EntityFrameworkCore;
using UserService.Domain.Entities;
using UserService.Domain.Repositories.PasswordResetCode;

namespace UserService.Infrastructure.DataAccess.Repositories;

internal class PasswordResetCodeRepository(UserServiceDbContext context) : IPasswordResetCodeRepository
{
    public async Task Add(PasswordResetCode passwordResetCode)
    {
        await context.PasswordResetCodes.AddAsync(entity: passwordResetCode);
    }

    public void Remove(PasswordResetCode passwordResetCode)
    {
        context.PasswordResetCodes.Remove(entity: passwordResetCode);
    }

    public void Update(PasswordResetCode passwordResetCode)
    {
        context.PasswordResetCodes.Update(entity: passwordResetCode);
    }

    public async Task<PasswordResetCode?> GetByUserId(Guid userId)
    {
        return await context.PasswordResetCodes.FirstOrDefaultAsync(predicate: p => p.UserId == userId);
    }
}